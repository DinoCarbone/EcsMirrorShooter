using ECS.Common.Lifecycle.Interfaces;
using ECS.Gameplay.Shooting.Interfaces;
using Networking.Mirror.Lifecycle;
using Networking.Mirror.Integration;
using Networking.Mirror.Shooting;
using UnityEngine;
using Zenject;

namespace DI
{
    public sealed class MirrorInstaller : MonoInstaller
    {
        [SerializeField] private ZenjectNetworkManager networkManager;
        [SerializeField] private MirrorBulletSpawnerDecorator bulletSpawnerPrefab;

        public override void InstallBindings()
        {
            Container
                .Decorate<IEntityDestroyer>()
                .With<MirrorEntityDestroyerDecorator>();

            Container
                .Decorate<IBulletSpawner>()
                .With<MirrorBulletSpawnerDecorator>()
                .FromMethod((_, originalSpawner) =>
                {
                    MirrorBulletSpawnerDecorator bulletSpawner =
                        Container.InstantiatePrefabForComponent<MirrorBulletSpawnerDecorator>(
                            bulletSpawnerPrefab);

                    bulletSpawner.Construct(originalSpawner);
                    return bulletSpawner;
                });

            Container.BindInterfacesAndSelfTo<ZenjectNetworkManager>()
                .FromNewComponentOnNewPrefab(networkManager).AsSingle().NonLazy();
        }
    }
}
