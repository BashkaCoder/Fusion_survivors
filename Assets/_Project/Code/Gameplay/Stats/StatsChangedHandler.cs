using System;
using Gameplay.CharacterBehaviour;
using Zenject;

namespace Gameplay.Stats
{
    public class StatsChangedHandler : IInitializable, IDisposable
    {
        private IStatsReadOnly _stats;
        
        private readonly CharacterMovement _movement;
        private readonly CharacterAutoAttack _attack;
        private readonly CharacterHealth _health;

        public StatsChangedHandler(
            IStatsReadOnly stats, 
            CharacterMovement movement, 
            CharacterAutoAttack attack, 
            CharacterHealth health)
        {
            _stats = stats;
            _movement = movement;
            _attack = attack;
            _health = health;
        }
        
        public void Initialize() => _stats.Changed += HandleStatChanged;

        public void Dispose() => _stats.Changed -= HandleStatChanged;

        private void HandleStatChanged(StatId statId)
        {
            switch (statId)
            {
                case StatId.MoveSpeed:
                    _movement.SetMoveSpeed(_stats.Get(StatId.MoveSpeed));
                    break;
                case StatId.AttackDamage:
                    _attack.SetAttackDamage(_stats.Get(StatId.AttackDamage));
                    break;
                case StatId.AttackSpeed:
                    _attack.SetAttackSpeed(_stats.Get(StatId.AttackSpeed));
                    break;
                case StatId.MaxHealth:
                    _health.SetMaxHealth(_stats.Get(StatId.MaxHealth));
                    break;
            }
        }
    }
}