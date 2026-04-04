using System;
using ScriptableObjects;

namespace Gameplay.Stats.Progression
{
    public class PlayerProgression : IPlayerProgression
    {
        public int Level { get; private set; }
        public float CurrentXp { get; private set; }
        public float Threshold { get; }
        
        public event Action Changed;
        public event Action LeveledUp;
        
        public PlayerProgression(PlayerLevelConfig config)
        {
            Threshold = config.XpThreshold;
        }
        
        public void AddXp(float amount)
        {
            CurrentXp += amount;
            Changed?.Invoke();

            while (CurrentXp >= Threshold)
            {
                CurrentXp -= Threshold;
                Level++;
                LeveledUp?.Invoke();
            }
        }
    }
}