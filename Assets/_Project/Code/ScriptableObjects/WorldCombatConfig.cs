using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WorldCombatConfig", menuName = "ScriptableObjects/WorldCombatConfig")]
    public class WorldCombatConfig : ScriptableObject
    {
        [field: SerializeField, Min(1f)] public float ProjectileSpeed { get; private set; }
        [field: SerializeField, Min(0f)] public float SpawnCooldown { get; private set; }
        [field: SerializeField, Min(0f)] public float ExperienceForKilledEnemy { get; private set; }
    }
}