using Infrastructure.Pools;
using ScriptableObjects;
using UnityEngine;

namespace Infrastructure.Spawners
{
    public class BulletSpawner
    {
        private BulletPool _pool;
        private WorldCombatConfig _worldCombatConfig;
        
        public BulletSpawner(BulletPool pool, WorldCombatConfig worldCombatConfig)
        {
            _pool = pool;
            _worldCombatConfig = worldCombatConfig;
        }

        public void Spawn(Vector3 position, Vector2 direction, float damage)
        {
            var bullet = _pool.Spawn();
            bullet.transform.SetPositionAndRotation(position, Quaternion.FromToRotation(Vector3.up, direction));
            bullet.SetDamage(damage);
            bullet.SetSpeed(_worldCombatConfig.ProjectileSpeed);
        }
    }
}