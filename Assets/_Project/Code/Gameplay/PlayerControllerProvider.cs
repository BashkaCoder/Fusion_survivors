using System.Collections.Generic;

namespace Gameplay
{
    public class PlayerControllerProvider
    {
        private readonly HashSet<PlayerController> _players = new();
        
        public ICollection<PlayerController> Players => _players;
        
        public void Register(PlayerController player)
        {
            _players.Add(player);
        }

        public void Unregister(PlayerController player)
        {
            _players.Remove(player);
        }
    }
}