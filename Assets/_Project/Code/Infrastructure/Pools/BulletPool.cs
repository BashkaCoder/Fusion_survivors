using Gameplay;
using Zenject;

namespace Infrastructure.Pools
{
    public class BulletPool : MemoryPool<Bullet>
    {
        protected override void OnCreated(Bullet item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Bullet item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Bullet item)
        {
            item.gameObject.SetActive(false);
        }
    }
}