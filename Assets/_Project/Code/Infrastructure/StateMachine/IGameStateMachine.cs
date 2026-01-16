using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void SwitchState<TState>() where TState : IState;
    }
}