using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerLevelConfig", menuName = "ScriptableObjects/PlayerLevelConfig")]
    public class PlayerLevelConfig: ScriptableObject
    {
        [field: SerializeField] public float XpThreshold {get; private set;}
    }
}