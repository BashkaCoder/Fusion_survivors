using Infrastructure.SceneLoader;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class ReturnToMenuState : IState
    {
        
        private readonly ISceneLoader _sceneLoader;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        
        public ReturnToMenuState(ISceneLoader sceneLoader, LazyInject<IGameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log($"Enter {nameof(ReturnToMenuState)}");
            _sceneLoader.LoadScene(SceneKey.MainMenu);
            _gameStateMachine.Value.SwitchState<MenuState>();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(ReturnToMenuState)}");
        }
    }
}