using Gameplay.CharacterBehaviour;
using Gameplay.Stats;
using Infrastructure.Input;
using ScriptableObjects;
using Zenject;

namespace Infrastructure.Installers
{
    public class EnemyPrefabInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NearestPlayerDirectionProvider>().AsSingle();
            
            Container.Bind<CharacterMovement>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterAnimator>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterHealth>().FromComponentOnRoot().AsSingle();
            Container.Bind<CharacterMeleeAttack>().FromComponentOnRoot().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CharacterStatsRuntime>()
                .FromMethod(_ =>
                {
                    var config = Container.ResolveId<CharacterStatsConfig>(CharacterId.Enemy);
                    return new CharacterStatsRuntime(config);
                })
                .AsSingle();
        }
    }
}
