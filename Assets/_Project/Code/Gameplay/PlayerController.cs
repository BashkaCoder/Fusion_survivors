using Fusion;
using Gameplay.CharacterBehaviour;
using Fusion.Addons.Physics;
using Gameplay.Stats;
using Gameplay.Stats.Progression;
using Gameplay.Stats.UI;
using Gameplay.Stats.Upgrade;
using Infrastructure;
using Infrastructure.Installers;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using NetCode;
using ScriptableObjects;
using UnityEngine;
using Zenject;
    
namespace Gameplay
{
    //TODO: Почему NetworkBehaviour?
    public class PlayerController : NetworkBehaviour
    {
        //TODO: приписки "_player" - кринж?
        [SerializeField] private CharacterMovement _playerMovement;
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] private CharacterAutoAttack _playerAttack;
        [SerializeField] private CharacterHealth _playerHealth;
        [SerializeField] private NetworkedPlayerInfoView _networkedPlayerInfoView;
        [SerializeField] private PlayerVisuals _playerVisuals;
        [SerializeField] private PlayerXpCollector _xpCollector;
        [SerializeField] private NetworkRigidbody2D _networkRigidbody;
        
        private CharacterStatsRuntime _playerStats;
        private IPlayerProgression _playerProgression;
        private IUpgradePicker _upgradePicker;
        private Transform _defaultInterpolationTarget;
        
        //TODO: Включена должна быть только одна из вьюх - зависит от роли: хост/клиент
        private PlayerInfoView _playerInfoView; //From SceneContainer
        private PlayerControllerProvider  _playerControllerProvider;
        //TODO: А почему тут? Мб лучше будет в том месте, где спавнит(PlayerSpawner/PlayerPool)
        private BannedPlayersService _bannedPlayersService;
        private LazyInject<IGameStateMachine> _gameStateMachine;
        
        private CharacterStatsConfig _characterStatsConfig;
        private PlayerLevelConfig _playerLevelConfig;
        private UpgradeValuesConfig _upgradeValuesConfig;
        
        private bool _isInitialized;
        
        [Networked] private NetworkBool IsHost { get; set; }
        [Networked, OnChangedRender(nameof(OnNicknameChanged))] private string Nickname { get; set; }
        
        [Inject]
        private void Construct(
            PlayerInfoView playerInfoView,
            PlayerControllerProvider playerControllerProvider,
            BannedPlayersService bannedPlayersService,
            LazyInject<IGameStateMachine> gameStateMachine,
            [Inject(Id = CharacterId.Player)] CharacterStatsConfig characterStatsConfig,
            PlayerLevelConfig playerLevelConfig,
            UpgradeValuesConfig upgradeValuesConfig)
        {
            _playerInfoView = playerInfoView;
            _playerControllerProvider = playerControllerProvider;
            _bannedPlayersService = bannedPlayersService;
            _gameStateMachine = gameStateMachine;
            _characterStatsConfig = characterStatsConfig;
            _playerLevelConfig = playerLevelConfig;
            _upgradeValuesConfig = upgradeValuesConfig;
        }

        //TODO: Ревизия
        public void SetSpawnData(PlayerSpawnData spawnData)
        {
            //if (!Object.HasStateAuthority) return;
            
            IsHost = spawnData.IsHost;
            Nickname = spawnData.Nickname;
        }

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                RPC_SetNickname(PlayerPrefs.GetString("PlayerName"));
            }
            OnNicknameChanged();
            
            if (Runner != null)
            {
                Runner.SetIsSimulated(Object, ShouldSimulateLocally());
            }
            ConfigureRuntimeSmoothing();
            InitializeOnce(); //TODO: Почему once в названии?
        }
        
        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            if (runner != null)
            {
                runner.SetIsSimulated(Object, false);
            }

            Unsubscribe();
            _playerControllerProvider?.Unregister(this);
        }

        private void InitializeOnce()
        {
            SetupVisuals();
            SetupRuntime();
            Subscribe();

            _playerControllerProvider.Register(this);
        }

        private void ConfigureRuntimeSmoothing()
        {
            if (_networkRigidbody == null)
            {
                return;
            }

            _networkRigidbody.InterpolationTarget = ShouldUseInterpolationTarget()
                ? _defaultInterpolationTarget
                : null;
        }

        private bool ShouldSimulateLocally()
        {
            return Object != null && (Object.HasStateAuthority || Object.HasInputAuthority);
        }

        private bool ShouldUseInterpolationTarget()
        {
            return _defaultInterpolationTarget != null && Object != null && !Object.HasStateAuthority;
        }
        
        private void SetupVisuals()
        {
            _playerVisuals.Setup(IsHost);
            _networkedPlayerInfoView.Setup(Nickname);
            _networkedPlayerInfoView.gameObject.SetActive(!HasInputAuthority);
        }
        
        private void SetupRuntime()
        {
            _playerStats = new CharacterStatsRuntime(_characterStatsConfig);
            _upgradePicker = new RandomUpgradePicker(_upgradeValuesConfig);
            _playerProgression = new PlayerProgression(_playerLevelConfig);

            _xpCollector.Setup(_playerProgression);
            
            InitializePlayerStats();
            ResetPlayerInfoView();
        }
        
        private void Subscribe()
        {
            _playerProgression.Changed += HandleExperienceChanged;
            _playerProgression.LeveledUp += HandleLevelUp;
            _playerStats.Changed += HandleStatChanged;
            _playerHealth.Changed += HandleHealthChanged;
            _playerHealth.OnDied += HandlePlayerDeath;
        }
        
        private void Unsubscribe()
        {
            _playerProgression.Changed -= HandleExperienceChanged;
            _playerProgression.LeveledUp -= HandleLevelUp;
            _playerStats.Changed -= HandleStatChanged;
            _playerHealth.Changed -= HandleHealthChanged;
            _playerHealth.OnDied -= HandlePlayerDeath;
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
        }
        
        private void HandleExperienceChanged()
        {
            _playerInfoView.SetExperience(_playerProgression.CurrentXp, _playerProgression.Threshold);
        }
        
        private void HandleLevelUp()
        {
            _upgradePicker.GetUpgrade().ApplyTo(_playerStats);
            _playerHealth.RestoreToMax();
            _playerInfoView.SetLevel(_playerProgression.Level.ToString());
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
            // if (Object.HasStateAuthority)
            // {
            _bannedPlayersService.Add(Nickname);
            // }
            
            _gameStateMachine.Value.SwitchState<ReturnToMenuState>();
        }
        
        private void OnNicknameChanged()
        {
            if (HasInputAuthority)
            {
                return;
            }

            _networkedPlayerInfoView.Setup(Nickname);
        }

        //TODO: Вопрос к РПЦ? Потому что ведь Nickname у нас [Networked]
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void RPC_SetNickname(string nickname)
        {
            Nickname = nickname;
        }
    }
}
