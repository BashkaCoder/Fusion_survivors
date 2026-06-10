using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Fusion;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using NetCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _connectionStatusText;
        [SerializeField] private TMP_InputField _roomNameInput;
        [SerializeField] private TMP_InputField _nicknameInput;
        [SerializeField] private Button _startGameButton;
        
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;

        public Action<string, string> StartGameRequested; //Action<roomName, nickName>
        
        private RoomService _roomService;
        
        private IGameStateMachine _gameStateMachine;
        private FusionSessionService _fusionSessionService;
        private NetworkEvents _networkEvents;
        
        [Inject]
        private void Construct(
            IGameStateMachine gameStateMachine,
            FusionSessionService fusionSessionService)
        {
            _gameStateMachine = gameStateMachine;
            _fusionSessionService = fusionSessionService;
            _networkEvents = _fusionSessionService.NetworkEvents;
        }

        private void Awake()
        {
            _roomService = new RoomService(_fusionSessionService.NetworkEvents);
            _networkEvents.OnSessionListUpdate.AddListener(ShowSessionInfo);
            
            _fusionSessionService.EnsureLobbyJoinedAsync().Forget();
        }

        private void OnDestroy()
        {
            _networkEvents.OnSessionListUpdate.RemoveListener(ShowSessionInfo);
            _roomService.Dispose();
        }

        private void OnEnable()
        {
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinExistingRoom);
            //_startGameButton.onClick.AddListener(RequestGameStart);
        }

        private void OnDisable()
        {
            _createRoomButton.onClick.RemoveListener(CreateRoom);
            _joinRoomButton.onClick.RemoveListener(JoinExistingRoom);
            //_startGameButton.onClick.RemoveListener(RequestGameStart);
        }

        private void CreateRoom()
        {
            //Switch to Connect State
            //NetworkRunner create room
            //throw new NotImplementedException();
        
            //LoadGameplayScene();
            _fusionSessionService.StartPreparedSessionAsync().Forget();
        }

        private void JoinExistingRoom()
        {
            //Switch to Connect State
            //NetworkRunner find room. If exists - join
            //throw new NotImplementedException();
        
            //LoadGameplayScene();
            _fusionSessionService.StartPreparedSessionAsync().Forget();
        }

        private void RequestGameStart()
        {
            StartGameRequested.Invoke(_roomNameInput.text, _nicknameInput.text);
        }
        
        private void LoadGameplayScene()
        {
            _gameStateMachine.SwitchState<GameplayLoadState>();
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