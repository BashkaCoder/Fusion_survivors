namespace Gameplay.Stats.Upgrade
{
    public readonly struct StatUpgrade
    {
        private readonly StatId _statId;
        private readonly UpgradeType _type;
        private readonly float _value;

        public StatUpgrade(StatId statId, UpgradeType type, float value)
        {
            _statId = statId;
            _type = type;
            _value = value;
        }

        public void ApplyTo(CharacterStatsRuntime stats)
        {
            if (_type == UpgradeType.Add)
                stats.AddFlat(_statId, _value);
            else if (_type == UpgradeType.Multiply)
                stats.AddMultiplier(_statId, _value);
        }
    }
}