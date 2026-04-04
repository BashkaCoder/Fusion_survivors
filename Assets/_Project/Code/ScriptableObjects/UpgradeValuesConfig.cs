using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "UpgradeValuesConfig", menuName = "ScriptableObjects/UpgradeValuesConfig")]
    public class UpgradeValuesConfig : ScriptableObject
    {
        [field: SerializeField] public float AddMin {get; private set;}
        [field: SerializeField] public float AddMax {get; private set;}
        
        [field: SerializeField] public float MultiplyMin {get; private set;}
        [field: SerializeField] public float MultiplyMax {get; private set;}
    }
}