using ScriptableObjects;

namespace Gameplay.Stats.Upgrade
{
    public class RandomUpgradePicker : IUpgradePicker
    {
        private readonly UpgradeValuesConfig _config;

        public RandomUpgradePicker(UpgradeValuesConfig config)
        {
            _config = config;
        }
        
        private static readonly StatId[] AvailableStats =
        {
            StatId.MaxHealth,
            StatId.AttackDamage,
            StatId.AttackSpeed,
            StatId.MoveSpeed,
        };

        public StatUpgrade GetUpgrade() => CreateRandomUpgrade();

        private StatUpgrade CreateRandomUpgrade()
        {
            var statId = GetRandomStat();
            var type = GetRandomUpgradeType();
            var value = GetRandomValue(type);

            return new StatUpgrade(statId, type, value);
        }

        private StatId GetRandomStat() => AvailableStats[UnityEngine.Random.Range(0, AvailableStats.Length)];

        private UpgradeType GetRandomUpgradeType()
        {
            return UnityEngine.Random.value < 0.5f
                ? UpgradeType.Add
                : UpgradeType.Multiply;
        }

        private float GetRandomValue(UpgradeType type)
        {
            return type == UpgradeType.Add 
                ? UnityEngine.Random.Range(_config.AddMin, _config.AddMax) 
                : UnityEngine.Random.Range(_config.MultiplyMin, _config.MultiplyMax);
        }
    }
}