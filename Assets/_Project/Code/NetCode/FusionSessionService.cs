using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.Installers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NetCode
{
    public class FusionSessionService
    {
        //TODO: Не уверене, что консты должны быть здесь
        private const string AliveNicknamesProperty = "alive_nicknames";
        private const string BannedNicknamesProperty = "banned_nicknames";
        
        private readonly NetworkRunner _runnerPrefab;
        private readonly ScenesConfig _scenesConfig;
        
        private NetworkRunner _runnerInstance;
        private NetworkEvents _networkEvents;

        public NetworkEvents NetworkEvents { get { EnsureInstance(); return _networkEvents; } }

        public FusionSessionService(ScenesConfig scenesConfig, NetworkRunner runnerPrefab)
        {
            _scenesConfig = scenesConfig;
            _runnerPrefab = runnerPrefab;
        }

        //TODO: Вызывать
        public event Action<NetworkRunner, List<SessionInfo>> SessionListChanged;
        
        public async UniTask EnsureLobbyJoinedAsync()
        {
            EnsureInstance();
            await _runnerInstance.JoinSessionLobby(SessionLobby.ClientServer);
        }

        public void PrepareHost()
        {
            EnsureInstance();
            //TODO: Реализовать
        }
        
        public void PrepareJoin()
        {
            EnsureInstance();
            //TODO: Реализовать
        }

        public async UniTask StartPreparedSessionAsync()
        {
            EnsureInstance();
            
            var customProperties = new Dictionary<string, SessionProperty>
            {
                [AliveNicknamesProperty] = "",
                [BannedNicknamesProperty] = ""
            };
            
            var networkSceneInfo = new NetworkSceneInfo();
            networkSceneInfo.AddSceneRef(_scenesConfig.GameplaySceneRef);
            
            var result = await _runnerInstance.StartGame(new StartGameArgs
            {
                Scene = networkSceneInfo,
                GameMode = GameMode.AutoHostOrClient,
                SessionName = "Kyrka",
                SessionProperties = customProperties,
            });

            if (!result.Ok)
            {
                Debug.LogError($"StartGame failed. Reason: {result.ShutdownReason}");
            }
        }

        public async UniTask ShutdownToLobbyAsync()
        {
            //TODO: Реализовать
        }

        private void EnsureInstance()
        {
            if (_runnerInstance != null)
            {
                return;
            }

            _runnerInstance = Object.Instantiate(_runnerPrefab);
            _networkEvents = _runnerInstance.GetComponent<NetworkEvents>();
            Object.DontDestroyOnLoad(_runnerInstance.gameObject);
        }
    }
}
