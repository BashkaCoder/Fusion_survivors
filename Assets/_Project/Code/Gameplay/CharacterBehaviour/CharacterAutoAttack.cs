using Infrastructure.Input;
using Infrastructure.Spawners;
using UnityEngine;
using Zenject;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterAutoAttack : MonoBehaviour
    {
        private IMoveDirectionProvider _moveDirectionProvider;
        private BulletSpawner _bulletSpawner;
        
        private float _attackTimer;

        private Vector2 _attackDirection;
        private float _attackSpeed;
        private float _attackDamage;

        private float AttackCooldown => 1 / _attackSpeed;

        [Inject]
        private void Construct(IMoveDirectionProvider moveDirectionProvider, BulletSpawner bulletSpawner)
        {
            _moveDirectionProvider = moveDirectionProvider;
            _bulletSpawner = bulletSpawner;
        }

        private void Update() => ShotRoutine();
        
        private void ShotRoutine()
        {
            var attackDirection = _moveDirectionProvider.GetDirection();
            if (attackDirection != Vector2.zero) _attackDirection = attackDirection;
            
            _attackTimer += Time.deltaTime;
            if (!(_attackTimer > AttackCooldown)) return;
            
            ShotAtDirection(_attackDirection);
            _attackTimer = 0f;
        }
        
        private void ShotAtDirection(Vector2 direction)
        {
            _bulletSpawner.Spawn(transform.position, direction, _attackDamage);
        }
        
        public void SetAttackSpeed(float speed) => _attackSpeed = speed;
        
        public void SetAttackDamage(float damage) => _attackDamage = damage;
    }
}