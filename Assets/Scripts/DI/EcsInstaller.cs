using ECS.Common.Lifecycle.Interfaces;
using ECS.Common.Lifecycle.Services;
using ECS.Gameplay.Camera.Interfaces;
using ECS.Gameplay.Camera.Services;
using ECS.Gameplay.Shooting.Interfaces;
using ECS.Gameplay.Shooting.Systems;
using ECS.Startup;
using Unity.Cinemachine;
using Zenject;

namespace DI
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsWorldProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
            Container.Bind<IEcsEntityFactory>().To<EcsEntityFactory>().AsSingle();
            Container.Bind<IEntityDestroyer>().To<UnityEntityDestroyer>().AsSingle();
            Container.Bind<IBulletSpawner>().To<BulletSpawner>().AsSingle();
            Container.Bind<CinemachineCamera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IPlayerCameraBinder>().To<CinemachinePlayerCameraBinder>().AsSingle();
        }
    }
}
