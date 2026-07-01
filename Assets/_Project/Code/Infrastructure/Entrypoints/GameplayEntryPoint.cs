using Fusion;
using Gameplay;
using Infrastructure.Input;
using NetCode;
using UnityEngine;
using Zenject;

namespace Infrastructure.Entrypoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private FusionSessionService _fusionSessionService;
        private BannedPlayersService _bannedPlayersService;
        private BannedPlayersView _bannedPlayersView;
        
        [SerializeField] private NetworkObject _playerControllerPrefab;
        [SerializeField] private BoxCollider2D _spawnBounds;
        
        private IMoveDirectionProvider _moveDirectionProvider;
        
        [Inject]
        private void Construct(
            FusionSessionService fusionSessionService,
            BannedPlayersService bannedPlayersService,
            BannedPlayersView bannedPlayersView)
        {
            _fusionSessionService = fusionSessionService;
            _bannedPlayersService = bannedPlayersService;
            _bannedPlayersView = bannedPlayersView;
        }
        
        private void Awake()
        {
            _bannedPlayersService.Changed += _bannedPlayersView.HandleBannedPlayedAdded;
            
            _moveDirectionProvider = CreateMoveDirectionProvider();

            _fusionSessionService.NetworkEvents.PlayerJoined.AddListener(HandlePlayerJoined);
            _fusionSessionService.NetworkEvents.PlayerLeft.AddListener(HandlePlayerLeft);
            _fusionSessionService.NetworkEvents.OnInput.AddListener(HandleInput);
        }

        private void OnDestroy()
        {
            _bannedPlayersService.Changed -= _bannedPlayersView.HandleBannedPlayedAdded;

            _fusionSessionService.NetworkEvents.PlayerJoined.RemoveListener(HandlePlayerJoined);
            _fusionSessionService.NetworkEvents.PlayerLeft.RemoveListener(HandlePlayerLeft);
            _fusionSessionService.NetworkEvents.OnInput.RemoveListener(HandleInput);
        }

        private void HandlePlayerJoined(NetworkRunner runner, PlayerRef player)
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
                        Nickname = PlayerPrefs.GetString("PlayerName"),
                    });
                });

            runner.SetPlayerObject(player, playerObject);
        }

        private void HandlePlayerLeft(NetworkRunner runner, PlayerRef player)
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

        private void HandleInput(NetworkRunner runner, NetworkInput input)
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

        private Vector3 GetSpawnPosition()
        {
            var bounds = _spawnBounds.bounds;

            return bounds.center + new Vector3(
                (Random.value - 0.5f) * bounds.size.x,
                (Random.value - 0.5f) * bounds.size.y,
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
