using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class NetworkedPlayerInfoView : MonoBehaviour
    {
        [SerializeField] private UINameplate _nameplate;
        [SerializeField] private Slider _healthSlider;
        
        public void Setup(string nickname)
        {
            _nameplate.Setup(nickname);
        }

        public void SetHealth(float current, float maxHealth)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = current;
        }
    }
}