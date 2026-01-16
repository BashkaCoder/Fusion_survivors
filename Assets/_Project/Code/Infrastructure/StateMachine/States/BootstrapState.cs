using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        
        public BootstrapState(LazyInject<IGameStateMachine>  gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        public void Enter()
        {
            _gameStateMachine.Value.SwitchState<MenuState>();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(BootstrapState)}");
            //throw new System.NotImplementedException();
        }
    }
}