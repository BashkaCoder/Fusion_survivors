using Gameplay.Stats.Progression;
using UnityEngine;

namespace Gameplay.CharacterBehaviour
{
    public class PlayerXpCollector : MonoBehaviour
    {
        private IPlayerProgression _playerProgression;
        
        public void Setup(IPlayerProgression playerProgression)
        {
            _playerProgression = playerProgression;
        }
        
        public void AddXp(float xpToAdd)
        {
            _playerProgression.AddXp(xpToAdd);
        }
    }
}