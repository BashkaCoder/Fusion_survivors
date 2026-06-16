using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

namespace NetCode
{
    public class RoomNicknamesService
    {
        private const string AliveNicknamesProperty = "alive_nicknames";
        private const string BannedNicknamesProperty = "banned_nicknames";
        
        //TODO: Проверить в вызывающем коде if (Runner.SessionInfo == null) return;
        //TODO: Зачем каждый раз внутри создавать словрик?
        public void ClearNicknamesForSession(SessionInfo session)
        {
            var propertiesToUpdate = new Dictionary<string, SessionProperty>();
            
            session.UpdateCustomProperties(propertiesToUpdate);
        }
        
        //TODO: Проверить в вызывающем коде if (Runner.SessionInfo == null) return;
        //TODO: Зачем каждый раз внутри создавать словрик?
        public void AddNicknameAlive(SessionInfo session, string nickname)
        {
            var propertiesToUpdate = new Dictionary<string, SessionProperty>();
            
            var aliveNicknames = NicknameSerializer.DeserializeNicknames(session.Properties[AliveNicknamesProperty]);
            propertiesToUpdate[AliveNicknamesProperty] = NicknameSerializer.SerializeNicknames(aliveNicknames.Append(nickname).ToArray());
            Debug.Log($"a: {nickname}");
            
            session.UpdateCustomProperties(propertiesToUpdate);
        }
        
        //TODO: Проверить в вызывающем коде if (Runner.SessionInfo == null) return;
        //TODO: Зачем каждый раз внутри создавать словрик?
        public void AddNicknameBanned(SessionInfo session, string nickname)
        {
            var propertiesToUpdate = new Dictionary<string, SessionProperty>();
            
            var bannedNicknames = NicknameSerializer.DeserializeNicknames(session.Properties[BannedNicknamesProperty]);
            propertiesToUpdate[BannedNicknamesProperty] = NicknameSerializer.SerializeNicknames(bannedNicknames.Append(nickname).ToArray());
            Debug.Log($"b: {nickname}");
            
            session.UpdateCustomProperties(propertiesToUpdate);
        }
    }
}