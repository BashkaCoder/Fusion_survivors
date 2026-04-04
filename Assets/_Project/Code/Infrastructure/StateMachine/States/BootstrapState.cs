using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        //TODO: LazyInject. Циклическая зависимость между GameStateMachine и стейтами. Попахивает говной
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        
        public BootstrapState(LazyInject<IGameStateMachine>  gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        public void Enter()
        {
            _gameStateMachine.Value.SwitchState<MenuState>();
        }

        public void Exit()
        {
            Debug.Log($"Exit {nameof(BootstrapState)}");
        }
    }
}