using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class NetworkedPlayerInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nicknameLabel;
        [SerializeField] private Slider _healthSlider;

        public string Nickname { get; private set; }
        
        public void Setup(string nickname)
        {
            Nickname = nickname;
            _nicknameLabel.text = Nickname;
        }

        public void SetHealth(float current, float maxHealth)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = current;
        }
    }
}