using System;
using System.Collections.Generic;
using Fusion;

namespace NetCode
{
    public class RoomService : IDisposable
    {
        //TODO: Дубляж в GameplayEntryPoint.cs. Посмотреть + избавиться
        private const string AliveNicknamesProperty = "alive_nicknames";
        private const string BannedNicknamesProperty = "banned_nicknames";

        private readonly Dictionary<string, RoomNicknames> _roomNicknames = new();

        private readonly NetworkEvents _networkEvents;

        public RoomService(FusionSessionService fusionSessionService)
        {
            _networkEvents = fusionSessionService.NetworkEvents;
            _networkEvents.OnSessionListUpdate.AddListener(HandleSessionListUpdate);
        }

        //TODO: Подумать тут отписываться или в деструкторе
        public void Dispose()
        {
            _networkEvents.OnSessionListUpdate.RemoveListener(HandleSessionListUpdate);
        }

        public IEnumerable<string> GetAliveNicknamesForRoom(string roomId)
        {
            return _roomNicknames.TryGetValue(roomId, out var roomNicknames) ? 
                roomNicknames.ALiveNicknames :
                Array.Empty<string>();
        }
        
        public IEnumerable<string> GetBannedNicknamesForRoom(string roomId)
        {
            return _roomNicknames.TryGetValue(roomId, out var roomNicknames) ? 
                roomNicknames.BannedNicknames :
                Array.Empty<string>();
        }
        
        //TODO: Оптимизировать потом
        private void HandleSessionListUpdate(NetworkRunner runner, List<SessionInfo> sessions)
        {
            _roomNicknames.Clear();

            foreach (var session in sessions)
            {
                var roomNicknames = new RoomNicknames();
                _roomNicknames[session.Name] = roomNicknames;

                FillNicknames(roomNicknames.ALiveNicknames, session, AliveNicknamesProperty);
                FillNicknames(roomNicknames.BannedNicknames, session, BannedNicknamesProperty);
            }
        }
        
        private static void FillNicknames(HashSet<string> target, SessionInfo session, string propertyName)
        {
            if (!session.Properties.TryGetValue(propertyName, out var property))
            {
                return;
            }

            string serializedNicknames = property;

            foreach (var nickname in NicknameSerializer.DeserializeNicknames(serializedNicknames))
            {
                target.Add(nickname);
            }
        }
    }
}