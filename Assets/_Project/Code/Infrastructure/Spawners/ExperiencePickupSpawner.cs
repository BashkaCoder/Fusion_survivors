using Infrastructure.Pools;
using ScriptableObjects;
using UnityEngine;

namespace Infrastructure.Spawners
{
    public class ExperiencePickupSpawner
    {
        private readonly ExperiencePickupPool _pool;
        private readonly WorldCombatConfig _worldCombatConfig;
        
        public ExperiencePickupSpawner(ExperiencePickupPool pool, WorldCombatConfig worldCombatConfig)
        {
            _pool = pool;
            _worldCombatConfig = worldCombatConfig;
        }

        public void Spawn(Vector3 position)
        {
            _pool.Spawn(position, _worldCombatConfig.ExperienceForKilledEnemy);
        }
    }
}