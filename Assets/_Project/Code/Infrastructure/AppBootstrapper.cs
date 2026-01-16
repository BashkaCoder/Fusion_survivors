using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure
{
    public class AppBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;
        
        public AppBootstrapper(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;
        
        public void Initialize() => _gameStateMachine.SwitchState<BootstrapState>();
    }
}