using System;
using System.Collections.Generic;

namespace NetCode
{
    //TODO: Уебиндить и использовать где-то FusionSessionService или LobbyService
    public class PlayerRegistry
    {
        // TODO: dict<string roomName; HashSet<string> playerNicknames>
        private readonly Dictionary<string, HashSet<string>> _playersByRoom = new();

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void Add(string roomName, string playerId)
        {
            if (!_playersByRoom.TryGetValue(roomName, out var players))
            {
                players = new HashSet<string>();
                _playersByRoom[roomName] = players;
            }

            players.Add(playerId);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void Remove(string roomName, string playerId)
        {
            _playersByRoom.TryGetValue(roomName, out var players); 
            players?.Remove(playerId);

            if (players?.Count == 0)
            {
                _playersByRoom.Remove(roomName);
            }
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public bool Contains(string roomName, string playerId)
        {
            return _playersByRoom.TryGetValue(roomName, out var players) &&
                   players.Contains(playerId);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public int GetCount(string roomName)
        {
            return _playersByRoom.TryGetValue(roomName, out var players)
                ? players.Count
                : 0;
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public IEnumerable<string> GetPlayers(string roomName)
        {
            return _playersByRoom.TryGetValue(roomName, out var players)
                ?  players
                : Array.Empty<string>();
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public bool HasRoom(string roomName)
        {
            return _playersByRoom.ContainsKey(roomName);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void ClearRoom(string roomName)
        {
            _playersByRoom.Remove(roomName);
        }
    }
}