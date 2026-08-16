using ECS.Common.Lifecycle.Interfaces;
using ECS.Common.Lifecycle.Services;
using ECS.Common.Camera.Services;
using ECS.Common.Cursor.Services;
using ECS.Common.Menu.Services;
using ECS.Gameplay.Shooting.Interfaces;
using ECS.Gameplay.Shooting.Systems;
using ECS.Startup;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace DI
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerMenuPrefab;
        [SerializeField] private Canvas playerMenuCanvas;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsWorldProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
            Container.Bind<IEcsEntityFactory>().To<EcsEntityFactory>().AsSingle();
            Container.Bind<IEntityDestroyer>().To<UnityEntityDestroyer>().AsSingle();
            Container.Bind<IBulletSpawner>().To<BulletSpawner>().AsSingle();
            Container.Bind<CinemachineCamera>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CinemachinePlayerCameraService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityPlayerCursorService>().AsSingle();
            Container.BindInstance(playerMenuPrefab)
                .WhenInjectedInto<UnityPlayerMenuService>();
            Container.BindInstance(playerMenuCanvas)
                .WhenInjectedInto<UnityPlayerMenuService>();
            Container.BindInterfacesAndSelfTo<UnityPlayerMenuService>().AsSingle();
        }
    }
}
