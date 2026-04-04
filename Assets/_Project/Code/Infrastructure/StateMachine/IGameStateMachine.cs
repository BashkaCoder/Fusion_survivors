using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    //TODO: Интерфейс лишний
    public interface IGameStateMachine
    {
        void SwitchState<TState>() where TState : IState;
    }
}