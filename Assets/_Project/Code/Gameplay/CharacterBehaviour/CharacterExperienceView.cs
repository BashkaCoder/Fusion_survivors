using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterExperienceView : MonoBehaviour
    {
        [SerializeField] private Slider _xpSlider;
        
        public void Refresh(float current, float max)
        {
            _xpSlider.maxValue = max;
            _xpSlider.value = current;
        }
    }
}