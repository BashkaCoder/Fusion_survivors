using Infrastructure.SceneLoader;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            Container.BindInterfacesAndSelfTo<SyncSceneLoader>().AsSingle();
            Container.BindInterfacesTo<AppBootstrapper>().AsSingle().NonLazy();
        }

        private void BindGameStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesTo<BootstrapState>().AsSingle();
            Container.BindInterfacesTo<MenuState>().AsSingle();
            Container.BindInterfacesTo<NetworkConnectState>().AsSingle();
            Container.BindInterfacesTo<GameplayLoadState>().AsSingle();
            Container.BindInterfacesTo<GameplayState>().AsSingle();
            Container.BindInterfacesTo<ReturnToMenuState>().AsSingle();
        }
    }
}