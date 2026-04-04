using System;
using Gameplay.Stats.Progression;
using Zenject;

namespace Gameplay.Stats.Upgrade
{
    public class PlayerUpgradeService : IInitializable, IDisposable
    {
        private readonly CharacterStatsRuntime _stats;
        private readonly IUpgradePicker _upgradePicker;
        private readonly IPlayerProgression _playerProgression;

        public PlayerUpgradeService(
            CharacterStatsRuntime stats,
            IUpgradePicker upgradePicker,
            IPlayerProgression playerProgression)
        {
            _stats = stats; 
            _upgradePicker = upgradePicker;
            _playerProgression = playerProgression;
        }
        
        public void Initialize() => _playerProgression.LeveledUp += HandleLevelUp;

        public void Dispose() => _playerProgression.LeveledUp -= HandleLevelUp;

        private void HandleLevelUp() => _upgradePicker.GetUpgrade().ApplyTo(_stats);
    }
}