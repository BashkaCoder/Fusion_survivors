using Infrastructure.SceneLoader;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class GameplayLoadState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        
        public GameplayLoadState(ISceneLoader sceneLoader, LazyInject<IGameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log($"Enter {nameof(GameplayLoadState)}");
            _sceneLoader.LoadScene(SceneKey.Gameplay);
            _gameStateMachine.Value.SwitchState<GameplayState>();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(GameplayLoadState)}");
            //throw new System.NotImplementedException();
        }
    }
}