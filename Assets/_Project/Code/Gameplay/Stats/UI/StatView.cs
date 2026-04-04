using TMPro;
using UnityEngine;

namespace Gameplay.Stats.UI
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueLabel;

        public void SetValue(string newValue)
        {
            _valueLabel.text = newValue;
        }
    }
}