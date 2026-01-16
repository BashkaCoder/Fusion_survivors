using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenuButtons : MonoBehaviour
    {
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;

        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        private void OnEnable()
        {
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinExistingRoom);
        }

        private void OnDisable()
        {
            _createRoomButton.onClick.RemoveListener(CreateRoom);
            _joinRoomButton.onClick.RemoveListener(JoinExistingRoom);
        }

        private void CreateRoom()
        {
            //Switch to Connect State
            //NetworkRunner create room
            //throw new NotImplementedException();
        
            LoadGameplayScene();
        }

        private void JoinExistingRoom()
        {
            //Switch to Connect State
            //NetworkRunner find room. If exists - join
            //throw new NotImplementedException();
        
            LoadGameplayScene();
        }
    
        private void LoadGameplayScene()
        {
            _gameStateMachine.SwitchState<GameplayLoadState>();
        }
    }
}