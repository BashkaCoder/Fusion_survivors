using System.Linq;
using Gameplay.CharacterBehaviour;
using Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class ExperiencePickup : MonoBehaviour
    {
        private float _xpToAdd;

        private ExperiencePickupPool _pool;
        
        [Inject]
        private void Construct(ExperiencePickupPool pool)
        {
            _pool = pool;
        }

        public void Setup(float xpToAdd)
        {
            _xpToAdd = xpToAdd;
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_pool.InactiveItems.Contains(this)) return;
            
            if (other.TryGetComponent<PlayerXpCollector>(out var playerXpCollector))
            {
                playerXpCollector.AddXp(_xpToAdd);
                _pool.Despawn(this);
            }
        }
    }
}
