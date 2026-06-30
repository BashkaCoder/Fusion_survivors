using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;

namespace NetCode
{
    public class JoinRequestService : IDisposable
    {
        private readonly FusionSessionService _fusionSessionService;
        private readonly RoomService _roomService;
        
        private List<SessionInfo> _activeSessions = new();
        
        public JoinRequestService(
            FusionSessionService fusionSessionService,
            RoomService roomService)
        {
            _fusionSessionService = fusionSessionService;
            _roomService = roomService;

            _fusionSessionService.NetworkEvents.OnSessionListUpdate.AddListener(HandleSessionListUpdate);
        }

        public void Dispose()
        {
            _fusionSessionService.NetworkEvents.OnSessionListUpdate.RemoveListener(HandleSessionListUpdate);
        }
        
        private void HandleSessionListUpdate(NetworkRunner runner, List<SessionInfo> sessions)
        {
            _activeSessions = sessions;
        }
        
        public JoinRequestResult RequestJoin(string roomName, string nickName)
        {
            if (_activeSessions.Exists(session => session.Name == roomName && session.PlayerCount == session.MaxPlayers))
            {
                return JoinRequestResult.DeniedFullRoom;
            }
            
            if (_activeSessions.Exists(session => session.Name == roomName) && 
                _roomService.GetAliveNicknamesForRoom(roomName).Contains(nickName))
            {
                return JoinRequestResult.DeniedNicknameDuplicate;
            }

            if (_activeSessions.Exists(session => session.Name == roomName) &&
                _roomService.GetBannedNicknamesForRoom(roomName).Contains(nickName))
            {
                return JoinRequestResult.DeniedNicknameBanned;
            }
            
            return JoinRequestResult.Accepted;
        }
    }
}