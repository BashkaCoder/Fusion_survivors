using Infrastructure.Pools;
using ScriptableObjects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.Spawners
{
    public class EnemySpawner : ITickable
    {
        private readonly EnemyPool _pool;
        private readonly WorldCombatConfig _worldCombatConfig;
        private readonly BoxCollider2D _spawnCollider;
        
        private float _spawnTimer;
        
        public EnemySpawner(EnemyPool pool, WorldCombatConfig worldCombatConfig, BoxCollider2D spawnCollider)
        {
            _pool =  pool;
            _worldCombatConfig = worldCombatConfig;
            _spawnCollider = spawnCollider;
        }

        public void Tick()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _worldCombatConfig.SpawnCooldown)
            {
                _spawnTimer = 0f;
                Spawn();
            }  
        }
        
        private void Spawn()
        {
            _pool.Spawn(GetSpawnPosition());
        }

        private Vector3 GetSpawnPosition()
        {
            var bounds = _spawnCollider.bounds;

            return bounds.center + new Vector3(
                (Random.value - 0.5f) * bounds.size.x,
                (Random.value - 0.5f) * bounds.size.y,
                0f);
        }
    }
}
