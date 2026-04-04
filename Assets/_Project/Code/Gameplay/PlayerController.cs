using Gameplay.CharacterBehaviour;
using Gameplay.Stats;
using Gameplay.Stats.Progression;
using Gameplay.Stats.UI;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterMovement _playerMovement;
        private CharacterAutoAttack _playerAttack;
        private CharacterHealth _playerHealth;
        
        private CharacterStatsRuntime _playerStats;
        
        private IPlayerProgression _playerProgression;
        
        //TODO: Включена должна быть только одна из вьюх - зависит от роли: хост/клиент
        private PlayerInfoView _playerInfoView;
        private NetworkedPlayerInfoView _networkedPlayerInfoView;

        private PlayerControllerProvider  _playerControllerProvider;
        
        private LazyInject<IGameStateMachine> _gameStateMachine;
        
        private BannedPlayersInfo _bannedPlayersInfo;
        
        [Inject]
        private void Construct(
            CharacterMovement characterMovement, 
            CharacterAutoAttack characterAttack, 
            CharacterHealth characterHealth,
            CharacterStatsRuntime playerStats,
            IPlayerProgression playerProgression,
            PlayerInfoView playerInfoView,
            NetworkedPlayerInfoView networkedPlayerInfoView,
            PlayerControllerProvider playerControllerProvider,
            LazyInject<IGameStateMachine> gameStateMachine, 
            BannedPlayersInfo bannedPlayersInfo)
        {
            _playerMovement = characterMovement;
            _playerAttack = characterAttack;
            _playerHealth = characterHealth;
            _playerStats = playerStats;
            _playerProgression = playerProgression;
            _playerInfoView = playerInfoView;
            _networkedPlayerInfoView = networkedPlayerInfoView;
            _playerControllerProvider = playerControllerProvider;
            _gameStateMachine = gameStateMachine;
            _bannedPlayersInfo = bannedPlayersInfo;
        }
        
        private void Start()
        {
            InitializePlayerStats();
            ResetPlayerInfoView();  
            
            _playerProgression.Changed += HandleExperienceChanged;
            _playerProgression.LeveledUp += HandleLevelUp;
            _playerStats.Changed += HandleStatChanged;
            _playerHealth.Changed += HandleHealthChanged;
            _playerHealth.OnDied += HandlePlayerDeath;
            
            _playerControllerProvider.Register(this);
        }

        private void OnDestroy()
        {
            _playerProgression.Changed -= HandleExperienceChanged;
            _playerProgression.LeveledUp -= HandleLevelUp;
            _playerStats.Changed -= HandleStatChanged;
            _playerHealth.Changed -= HandleHealthChanged;
            _playerHealth.OnDied -= HandlePlayerDeath;
            
            _playerControllerProvider.Unregister(this);
        }

        private void InitializePlayerStats()
        {
            _playerAttack.SetAttackDamage(_playerStats.Get(StatId.AttackDamage));
            _playerAttack.SetAttackSpeed(_playerStats.Get(StatId.AttackSpeed));
            _playerMovement.SetMoveSpeed(_playerStats.Get(StatId.MoveSpeed));
            _playerHealth.SetMaxHealth(_playerStats.Get(StatId.MaxHealth));
            _playerHealth.RestoreToMax();
        }
        
        private void ResetPlayerInfoView()
        {
            HandleExperienceChanged();
            HandleLevelUp();
            HandleStatChanged(StatId.AttackDamage);
            HandleStatChanged(StatId.AttackSpeed);
            HandleStatChanged(StatId.MoveSpeed);
            HandleStatChanged(StatId.MaxHealth);
            HandleHealthChanged();
            
            //TODO: Tmp-Кринж
            _networkedPlayerInfoView.Setup("Bashka");
        }
        
        private void HandleExperienceChanged()
        {
            _playerInfoView.SetExperience(_playerProgression.CurrentXp, _playerProgression.Threshold);
        }
        
        private void HandleLevelUp()
        {
            _playerInfoView.SetLevel(_playerProgression.Level.ToString());
            _playerHealth.RestoreToMax();
        }
        
        private void HandleStatChanged(StatId statId)
        {
            var formattedValue = _playerStats.Get(statId).ToString("0.##");
            _playerInfoView.SetStat(statId, formattedValue);
        }
        
        private void HandleHealthChanged()
        {
            _playerInfoView.SetHealth(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
            _networkedPlayerInfoView.SetHealth(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
        }
        
        private void HandlePlayerDeath()
        {
            _bannedPlayersInfo.Add(_networkedPlayerInfoView.Nickname);
            _gameStateMachine.Value.SwitchState<ReturnToMenuState>();
        }
    }
}