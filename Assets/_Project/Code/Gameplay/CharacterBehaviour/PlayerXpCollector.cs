using Gameplay.Stats.Progression;
using UnityEngine;
using Zenject;

namespace Gameplay.CharacterBehaviour
{
    public class PlayerXpCollector : MonoBehaviour
    {
        private IPlayerProgression _playerProgression;

        [Inject]
        private void Construct(IPlayerProgression playerProgression)
        {
            _playerProgression = playerProgression;
        }
        
        public void AddXp(float xpToAdd)
        {
            _playerProgression.AddXp(xpToAdd);
        }
    }
}