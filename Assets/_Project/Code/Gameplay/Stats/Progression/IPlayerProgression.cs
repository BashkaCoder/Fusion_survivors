using System;

namespace Gameplay.Stats.Progression
{
    public interface IPlayerProgression
    {
        int Level { get; }
        float CurrentXp { get; }
        float Threshold { get; }
        
        event Action Changed;
        event Action LeveledUp;
        
        void AddXp(float amount);
    }
}