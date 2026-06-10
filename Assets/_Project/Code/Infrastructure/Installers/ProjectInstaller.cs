using Fusion;
using Infrastructure.SceneLoader;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using NetCode;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfig _scenesConfig;
        [SerializeField] private NetworkRunner _networkRunnerPrefab;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            Container.BindInstance(_scenesConfig).AsSingle();
            
            Container.BindInterfacesAndSelfTo<SyncSceneLoader>().AsSingle();
            Container.BindInterfacesTo<AppBootstrapper>().AsSingle().NonLazy();
            
            //TODO: Сомнительно. Мб в один из стейтов унести?
            Container.Bind<FusionSessionService>().AsSingle().WithArguments(_networkRunnerPrefab); 
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
