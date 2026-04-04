using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterMeleeAttack : MonoBehaviour
    {
        private float _attackSpeed;
        private float _attackDamage;

        private float AttackCooldown => 1 / _attackSpeed;
        
        private float _attackTimer;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            AttackIfHasHealth(other);
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            AttackIfHasHealth(other);
        }
        
        private void Update()
        {
            _attackTimer += Time.deltaTime;
        }

        private void AttackIfHasHealth(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out CharacterHealth characterHealth)) return;
            Attack(characterHealth);
        }
        
        private void Attack(CharacterHealth characterHealth)
        {
            if (_attackTimer < AttackCooldown) return;
            
            characterHealth.TakeDamage(_attackDamage);
            _attackTimer = 0;
        }
        
        public void SetAttackSpeed(float speed) => _attackSpeed = speed;
        
        public void SetAttackDamage(float damage) => _attackDamage = damage;
    }
}