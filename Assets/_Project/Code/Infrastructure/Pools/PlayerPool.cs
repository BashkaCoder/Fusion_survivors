using Gameplay;
using Zenject;

namespace Infrastructure.Pools
{
    public class PlayerPool : MemoryPool<PlayerSpawnData, PlayerController>
    {
        protected override void OnCreated(PlayerController item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(PlayerController item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(PlayerController item)
        {
            item.gameObject.SetActive(false);
        }
        
        protected override void Reinitialize(PlayerSpawnData spawnData, PlayerController item)
        {
            item.Initialize(spawnData);
        }
    }
}