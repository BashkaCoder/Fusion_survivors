using System.Linq;
using Gameplay.CharacterBehaviour;
using Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private float _damage;
        private float _speed;

        private BulletPool _pool;

        [Inject]
        private void Construct(BulletPool pool)
        {
            _pool = pool;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_pool.InactiveItems.Contains(this)) return;
            
            if (other.gameObject.TryGetComponent<CharacterHealth>(out var characterHealth))
                characterHealth.TakeDamage(_damage);
            
            _pool.Despawn(this);
        }

        private void Update() => MoveForward();

        private void MoveForward() => transform.Translate(Vector3.up * (_speed * Time.deltaTime));

        public void SetDamage(float damage) => _damage = damage;
        
        public void SetSpeed(float speed) => _speed = speed;
    }
}