using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterStatsConfig", menuName = "ScriptableObjects/CharacterStatsConfig")]
    public class CharacterStatsConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)] public float Health {get; private set; }
        [field: SerializeField, Min(0f)] public float AttackDamage {get; private set; }
        [field: SerializeField, Min(0f)] public float AttackSpeed {get; private set; }
        [field: SerializeField, Min(0f)] public float MoveSpeed {get; private set; }
    }
}