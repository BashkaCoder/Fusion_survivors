using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Gameplay;
using Infrastructure.Input;
using NetCode;
using UnityEngine;
using Zenject;

namespace Infrastructure.Entrypoints
{
    public class GameplayEntryPoint : MonoBehaviour, INetworkRunnerCallbacks
    {
        private BannedPlayersService _bannedPlayersService;
        private BannedPlayersView _bannedPlayersView;

        [SerializeField] private NetworkObject _playerControllerPrefab;
        [SerializeField] private BoxCollider2D _spawnBounds;
        
        private NetworkRunner _runner;
        private IMoveDirectionProvider _moveDirectionProvider;
        
        [Inject]
        private void Construct(
            BannedPlayersService bannedPlayersService,
            BannedPlayersView bannedPlayersView)
        {
            _bannedPlayersService = bannedPlayersService;
            _bannedPlayersView = bannedPlayersView;
        }
        
        private void Awake()
        {
            _bannedPlayersService.Changed += _bannedPlayersView.HandleBannedPlayedAdded;
            
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;
            _runner.AddCallbacks(this);
            _moveDirectionProvider = CreateMoveDirectionProvider();
        }

        private async void Start()
        {
            var result = await _runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = "TestRoom"
            });

            if (!result.Ok)
            {
                Debug.LogError($"StartGame failed. Reason: {result.ShutdownReason}");
                return;
            }
        }

        private void OnDestroy()
        {
            _bannedPlayersService.Changed -= _bannedPlayersView.HandleBannedPlayedAdded;

            if (_runner != null)
            {
                _runner.RemoveCallbacks(this);
            }
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer)
            {
                return;
            }

            //TODO: Убрать дядю?
            if (runner.GetPlayerObject(player) != null)
            {
                return;
            }

            var spawnPosition = GetSpawnPosition();
            var isHostPlayer = player == runner.LocalPlayer;

            var playerObject = runner.Spawn(
                _playerControllerPrefab,
                spawnPosition,
                Quaternion.identity,
                player,
                onBeforeSpawned: (_, playerNetworkObject) =>
                {
                    var controller = playerNetworkObject.GetComponent<PlayerController>();
                    controller.SetSpawnData(new PlayerSpawnData
                    {
                        SpawnPosition = spawnPosition,
                        IsHost = isHostPlayer,
                        Nickname = $"Player {player.PlayerId}",
                    });
                });

            runner.SetPlayerObject(player, playerObject);
            Debug.Log($"Spawned player object for {player}. HasInputAuthority: {playerObject != null && playerObject.HasInputAuthority}");
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer)
            {
                return;
            }

            var playerObject = runner.GetPlayerObject(player);
            if (playerObject != null)
            {
                runner.Despawn(playerObject);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var moveDirection = _moveDirectionProvider.GetDirection();
            if (moveDirection.sqrMagnitude > 1f)
            {
                moveDirection.Normalize();
            }

            input.Set(new GameplayInputData
            {
                MoveDirection = moveDirection,
            });
        }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

        private Vector3 GetSpawnPosition()
        {
            var bounds = _spawnBounds.bounds;

            return bounds.center + new Vector3(
                (UnityEngine.Random.value - 0.5f) * bounds.size.x,
                (UnityEngine.Random.value - 0.5f) * bounds.size.y,
                0f);
        }

        private IMoveDirectionProvider CreateMoveDirectionProvider()
        {
#if UNITY_STANDALONE
            return new KeyboardMoveDirectionProvider();
#elif UNITY_ANDROID
            return new MobileMoveDirectionProvider();
#else
            return new KeyboardMoveDirectionProvider();
#endif
        }
    }
}
