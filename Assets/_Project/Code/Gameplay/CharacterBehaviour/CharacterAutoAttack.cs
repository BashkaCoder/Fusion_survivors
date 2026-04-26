using Fusion;
using Infrastructure.Spawners;
using UnityEngine;
using Zenject;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterAutoAttack : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;

        private BulletSpawner _bulletSpawner;
        private NetworkObject _networkObject;

        private float _attackTimer;
        private Vector2 _attackDirection = Vector2.down;
        private float _attackSpeed;
        private float _attackDamage;

        private float AttackCooldown => 1 / _attackSpeed;

        [Inject]
        private void Construct(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }

        private void Awake()
        {
            _networkObject = GetComponentInParent<NetworkObject>();
        }

        private void Update() => ShotRoutine();

        private void ShotRoutine()
        {
            if (_networkObject != null && !_networkObject.HasStateAuthority)
            {
                return;
            }

            if (_characterMovement == null)
            {
                return;
            }

            var attackDirection = _characterMovement.LookDirection;
            if (attackDirection != Vector2.zero)
            {
                _attackDirection = attackDirection;
            }

            _attackTimer += Time.deltaTime;
            if (!(_attackTimer > AttackCooldown))
            {
                return;
            }

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
