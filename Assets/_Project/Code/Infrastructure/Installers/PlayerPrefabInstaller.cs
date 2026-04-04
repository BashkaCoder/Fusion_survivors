using Gameplay;
using Gameplay.CharacterBehaviour;
using Gameplay.Stats;
using Gameplay.Stats.Progression;
using Gameplay.Stats.Upgrade;
using Infrastructure.Input;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class PlayerPrefabInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
#if UNITY_STANDALONE
            Container.Bind<IMoveDirectionProvider>().To<KeyboardMoveDirectionProvider>().AsSingle();
#endif
#if UNITY_ANDROID
            Container.Bind<IMoveDirectionProvider>().To<MobileMoveDirectionProvider>().AsSingle();
#endif
            
            Container.Bind<CharacterMovement>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterAnimator>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterAutoAttack>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterHealth>().FromComponentOnRoot().AsSingle();
            Container.Bind<PlayerXpCollector>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<NetworkedPlayerInfoView>().FromComponentInChildren().AsSingle();
            
            Container.BindInterfacesAndSelfTo<StatsChangedHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerUpgradeService>().AsSingle();
            Container.Bind<IUpgradePicker>().To<RandomUpgradePicker>().AsSingle();
            Container.Bind<IPlayerProgression>().To<PlayerProgression>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CharacterStatsRuntime>()
                .FromMethod(_ =>
                {
                    var config = Container.ResolveId<CharacterStatsConfig>(CharacterId.Player);
                    return new CharacterStatsRuntime(config);
                })
                .AsSingle();
        }
    }
}