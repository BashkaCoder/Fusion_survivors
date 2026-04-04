using System.Collections.Generic;
using Gameplay.CharacterBehaviour;
using TMPro;
using UnityEngine;

namespace Gameplay.Stats.UI
{
    public class PlayerInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelValue;

        [SerializeField] private StatView _attackDamageView;
        [SerializeField] private StatView _attackSpeedView;
        [SerializeField] private StatView _maxHealthView;
        [SerializeField] private StatView _moveSpeedView;
        [SerializeField] private CharacterHealthView _playerHealthView;
        [SerializeField] private CharacterExperienceView _playerExperienceView;

        private Dictionary<StatId, StatView> _statsById;

        private void Awake()
        {
            _statsById = new Dictionary<StatId, StatView>()
            {
                { StatId.AttackDamage, _attackDamageView},
                { StatId.AttackSpeed, _attackSpeedView},
                { StatId.MaxHealth, _maxHealthView},
                { StatId.MoveSpeed, _moveSpeedView}
            };
        }
        
        public void SetLevel(string levelLabel)
        {
            _levelValue.text = levelLabel;
        }

        public void SetStat(StatId statId, string formattedValue)
        {
            _statsById[statId].SetValue(formattedValue);
        }

        public void SetHealth(float currentHealth, float maxHealth)
        {
            _playerHealthView.Refresh(currentHealth, maxHealth);
        }
        
        public void SetExperience(float currentXp, float maxXp)
        {
            _playerExperienceView.Refresh(currentXp, maxXp);
        }
    }
}