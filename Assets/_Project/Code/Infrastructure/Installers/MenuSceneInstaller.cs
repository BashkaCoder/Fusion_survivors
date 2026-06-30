using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameMenuUI _gameMenuUI;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gameMenuUI).AsSingle();
            Container.BindInterfacesAndSelfTo<GameMenuController>().AsSingle().NonLazy();
        }
    }
}
