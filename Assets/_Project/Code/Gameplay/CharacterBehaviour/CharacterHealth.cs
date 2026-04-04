using System;
using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class CharacterHealth : MonoBehaviour
    {
        public float CurrentHealth { get;  private set; }
        public float MaxHealth { get;  private set; }
        
        public event Action Changed;
        public event Action OnDied;
        
        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            Changed?.Invoke();
        }

        public void RestoreToMax()
        {
            CurrentHealth = MaxHealth;
            Changed?.Invoke();
        } 
        
        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            Changed?.Invoke();

            if (CurrentHealth <= 0)
            {
                OnDied?.Invoke();
            }
        }
    }
}