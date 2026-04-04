using Gameplay;
using UnityEngine;
using Zenject;

namespace Infrastructure.Pools
{
    public class ExperiencePickupPool : MemoryPool<Vector3, float, ExperiencePickup>
    {
        protected override void OnCreated(ExperiencePickup item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(ExperiencePickup item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(ExperiencePickup item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void Reinitialize(Vector3 position, float xp, ExperiencePickup item)
        {
            item.transform.position = position;
            item.Setup(xp);
        }
    }
}