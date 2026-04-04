using System;
using System.Collections.Generic;
using ScriptableObjects;

namespace Gameplay.Stats
{
    public class CharacterStatsRuntime : IStatsReadOnly
    {
        private readonly Dictionary<StatId, float> _base;
        private readonly Dictionary<StatId, float> _multiplicativeModifiers;
        private readonly Dictionary<StatId, float> _additiveModifiers;

        public event Action<StatId> Changed;

        public CharacterStatsRuntime(CharacterStatsConfig characterStatsConfig)
        {
            _base = new Dictionary<StatId, float>
            {
                [StatId.MoveSpeed] = characterStatsConfig.MoveSpeed,
                [StatId.AttackSpeed] = characterStatsConfig.AttackSpeed,
                [StatId.AttackDamage] = characterStatsConfig.AttackDamage,
                [StatId.MaxHealth] = characterStatsConfig.Health
            };

            _multiplicativeModifiers = new Dictionary<StatId, float>();
            _additiveModifiers = new Dictionary<StatId, float>();
        }
        
        public float Get(StatId stat)
        {
            var baseValue = _base.GetValueOrDefault(stat, 0f);
            var addedValue = _additiveModifiers.GetValueOrDefault(stat, 0f);
            var multiplier = _multiplicativeModifiers.GetValueOrDefault(stat, 1f);
            return (baseValue + addedValue) * multiplier;
        }

        public void AddFlat(StatId stat, float value)
        {
            _additiveModifiers[stat] = (_additiveModifiers.GetValueOrDefault(stat, 0f)) + value;
            Changed?.Invoke(stat);
        }

        public void AddMultiplier(StatId stat, float multiplier)
        {
            _multiplicativeModifiers[stat] = (_multiplicativeModifiers.GetValueOrDefault(stat, 1f)) * multiplier;
            Changed?.Invoke(stat);
        }
    }
}