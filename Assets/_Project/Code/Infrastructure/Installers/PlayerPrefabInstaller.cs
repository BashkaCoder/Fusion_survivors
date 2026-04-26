using Gameplay.Stats;
using Gameplay.Stats.Progression;
using Gameplay.Stats.Upgrade;
using Zenject;

namespace Infrastructure.Installers
{
    public class PlayerPrefabInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StatsChangedHandler>().AsSingle();
            Container.Bind<IUpgradePicker>().To<RandomUpgradePicker>().AsSingle();
            Container.Bind<IPlayerProgression>().To<PlayerProgression>().AsSingle();
        }
    }
}
