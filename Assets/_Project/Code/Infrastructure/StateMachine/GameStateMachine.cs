using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        
        private IState _currentState;

        public GameStateMachine(IEnumerable<IState> states)
        {
            foreach (var state in states)
            {
                var type = state.GetType();
                _states.Add(type, state);
            }
        }
        
        public void SwitchState<TState>() where TState : IState
        {
            var type = typeof(TState);
            var newState = _states[type];
            
            if (newState == _currentState) return;
            
            _currentState?.Exit(); //TODO: await?
            _currentState = newState;
            _currentState.Enter(); //TODO: await?
        }
    }
}