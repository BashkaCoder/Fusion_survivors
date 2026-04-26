using System;
using System.Collections.Generic;

namespace NetCode
{
    //TODO: Уебиндить и использовать где-то FusionSessionService или LobbyService
    public class BannedPlayerRegistry
    {
        // TODO: dict<string roomName; HashSet<string> bannedPlayerNicknames>
        private readonly Dictionary<string, HashSet<string>> _bannedPlayersByRoom = new();

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void Add(string roomName, string playerId)
        {
            if (!_bannedPlayersByRoom.TryGetValue(roomName, out var bannedPlayers))
            {
                bannedPlayers = new HashSet<string>();
                _bannedPlayersByRoom[roomName] = bannedPlayers;
            }

            bannedPlayers.Add(playerId);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void Remove(string roomName, string playerId)
        {
            _bannedPlayersByRoom.TryGetValue(roomName, out var bannedPlayers);
            bannedPlayers?.Remove(playerId);

            if (bannedPlayers?.Count == 0)
            {
                _bannedPlayersByRoom.Remove(roomName);
            }
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public bool Contains(string roomName, string playerId)
        {
            return _bannedPlayersByRoom.TryGetValue(roomName, out var bannedPlayers) &&
                   bannedPlayers.Contains(playerId);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public int GetCount(string roomName)
        {
            return _bannedPlayersByRoom.TryGetValue(roomName, out var bannedPlayers)
                ? bannedPlayers.Count
                : 0;
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public IEnumerable<string> GetBannedPlayers(string roomName)
        {
            return _bannedPlayersByRoom.TryGetValue(roomName, out var bannedPlayers)
                ? bannedPlayers
                : Array.Empty<string>();
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public bool HasRoom(string roomName)
        {
            return _bannedPlayersByRoom.ContainsKey(roomName);
        }

        //TODO: Проследить, чтобы тот кто вызывал - не передал пробел или нихуя. (string.IsNullOrWhiteSpace(var))
        public void ClearRoom(string roomName)
        {
            _bannedPlayersByRoom.Remove(roomName);
        }
    }
}