using ECS.Startup;
using Zenject;

namespace DI
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
            Container.Bind<IEcsEntityFactory>().To<EcsEntityFactory>().AsSingle();
        }
    }
}
