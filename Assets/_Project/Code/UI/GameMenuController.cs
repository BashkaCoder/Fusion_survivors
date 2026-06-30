using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using NetCode;
using UnityEngine;

namespace UI
{
    //Создать инстанс в руте сцены GameMenu
    //Вынести сюда из GameMenuUI то, что вьюхе не нужно
    public class GameMenuController : IDisposable
    {
        private readonly FusionSessionService _fusionSessionService;
        private readonly JoinRequestService _joinRequestService;
        private readonly RoomService _roomService;
        private readonly GameMenuUI _gameMenuUI;

        public GameMenuController(
            FusionSessionService fusionSessionService,
            JoinRequestService joinRequestService,
            RoomService roomService,
            GameMenuUI gameMenuUI)
        {
            _fusionSessionService = fusionSessionService;
            _joinRequestService = joinRequestService;
            _roomService = roomService;
            _gameMenuUI = gameMenuUI;

            _fusionSessionService.EnsureLobbyJoinedAsync().Forget();
            
            _gameMenuUI.StartGameRequested += HandleStartGameRequested;
            _fusionSessionService.NetworkEvents.OnSessionListUpdate.AddListener(ShowSessionInfo);
        }

        public void Dispose()
        {
            _gameMenuUI.StartGameRequested -= HandleStartGameRequested;
            _fusionSessionService.NetworkEvents.OnSessionListUpdate.RemoveListener(ShowSessionInfo);
        }

        // Реагировать на событие GameMenuUI.StartGameRequested
        // Если не играем - очистить UI. Если играем - начать подключение + сохранить ник в PlayerPrefs.SetString();
        private void HandleStartGameRequested(string roomName, string nickName)
        {
            var requestResult = _joinRequestService.RequestJoin(roomName, nickName);
            
            if (requestResult == JoinRequestResult.Accepted)
            {
                _fusionSessionService.StartPreparedSessionAsync(roomName).Forget();
                PlayerPrefs.SetString("PlayerName", nickName);
                return;
            }

            _gameMenuUI.HandleDeniedRequest(requestResult);
        }
        
        //TODO: TMP. Удалить!
        private void ShowSessionInfo(NetworkRunner runner, List<SessionInfo> sessions)
        {
            Debug.Log($"Sessions count: {sessions.Count}");

            foreach (var sessionInfo in sessions)
            {
                var aliveNicknames = _roomService.GetAliveNicknamesForRoom(sessionInfo.Name);
                var bannedNicknames = _roomService.GetBannedNicknamesForRoom(sessionInfo.Name);

                Debug.Log(
                    $"{sessionInfo.Name}\n" +
                    $"Alive: {string.Join(", ", aliveNicknames)}\n" +
                    $"Banned: {string.Join(", ", bannedNicknames)}");
            }
        }
    }
}