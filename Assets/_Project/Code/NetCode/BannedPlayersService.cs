using System;
using System.Collections.Generic;

namespace NetCode
{
    public class BannedPlayersService
    {
        private readonly HashSet<string> _bannedPlayerNicknames = new();

        public Action<string> Changed;
        
        public bool Contains(string nickname)
        {
            return _bannedPlayerNicknames.Contains(nickname);
        }
    
        public void Add(string nickname)
        {
            if (_bannedPlayerNicknames.Add(nickname))
            {
                Changed?.Invoke(nickname);
            }
        }

        public void Clear()
        {
            _bannedPlayerNicknames.Clear();
        }
    }
}