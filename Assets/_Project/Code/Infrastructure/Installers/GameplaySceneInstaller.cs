using Gameplay;
using Gameplay.Stats.UI;
using Infrastructure.Spawners;
using NetCode;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    //TODO: Если этот инсталлер висит на сцене, а игрок спавнится в рантайме - будет проблема.
    public class GameplaySceneInstaller : MonoInstaller
    {
        [Header("Player related instances")]
        [SerializeField] private PlayerInfoView _playerInfoView;
        [SerializeField] private BannedPlayersView _bannedPlayersView;
        
        [Header("Spawner bounds")]
        [SerializeField] private BoxCollider2D _mapBounds;
        
        public override void InstallBindings()
        {
            BindPlayerInstances();
            BindProviders();
            BindSpawners();
            
            Container.Bind<BannedPlayersService>().AsSingle();
        }

        private void BindProviders()
        {
            Container.Bind<PlayerControllerProvider>().AsSingle();
        }

        private void BindPlayerInstances()
        {
            Container.BindInstance(_playerInfoView).AsSingle();
            Container.BindInstance(_bannedPlayersView).AsSingle();
        }

        private void BindSpawners()
        {
            //Container.Bind<PlayerSpawner>().AsSingle().WithArguments(_mapBounds);
            //Container.BindInterfacesTo<EnemySpawner>().AsSingle().WithArguments(_mapBounds);
            Container.Bind<BulletSpawner>().AsSingle();
            Container.Bind<ExperiencePickupSpawner>().AsSingle();
        }
    }
}