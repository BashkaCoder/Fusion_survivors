using Gameplay;
using UnityEngine;
using Zenject;

namespace Infrastructure.Pools
{
    public class EnemyPool: MemoryPool<Vector3, EnemyController>
    {
        protected override void OnCreated(EnemyController item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(EnemyController item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(EnemyController item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void Reinitialize(Vector3 spawnPosition, EnemyController item)
        {
            item.gameObject.transform.position = spawnPosition;
        }
    }
}