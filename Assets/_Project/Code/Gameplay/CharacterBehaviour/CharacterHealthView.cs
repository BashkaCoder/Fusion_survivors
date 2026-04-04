using UnityEngine;
using UnityEngine.UI;
namespace Gameplay.CharacterBehaviour

{
    public class CharacterHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void Refresh(float current, float max)
        {
            _healthSlider.maxValue = max;
            _healthSlider.value = current;
        }
    }
}