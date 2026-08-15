using System;
using ECS.Common.Lifecycle.Interfaces;
using ECS.Gameplay.Shooting.Interfaces;
using Networking.Mirror.Lifecycle;
using Networking.Mirror.Integration;
using Networking.Mirror.Shooting;
using UnityEngine;
using Zenject;

namespace DI
{
    public class MirrorInstaller : MonoInstaller
    {
        [SerializeField] private ZenjectNetworkManager networkManager;

        public override void InstallBindings()
        {
            Container
                .Decorate<IEntityDestroyer>()
                .With<MirrorEntityDestroyerDecorator>();

            Container.Bind<IMirrorServerHandler>()
                .FromResolveGetter<IEntityDestroyer>(entityDestroyer =>
                    GetServerHandler(entityDestroyer))
                .AsCached();

            Container
                .Decorate<IBulletSpawner>()
                .With<MirrorBulletSpawnerDecorator>();

            Container.Bind<IMirrorServerHandler>()
                .FromResolveGetter<IBulletSpawner>(bulletSpawner =>
                    GetServerHandler(bulletSpawner))
                .AsCached();

            Container.Bind<IMirrorServerHandlersProxy>()
                .To<MirrorServerHandlersProxy>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<ZenjectNetworkManager>()
                .FromComponentInNewPrefab(networkManager).AsSingle().NonLazy();
        }

        private static IMirrorServerHandler GetServerHandler(object service)
        {
            if (service is IMirrorServerHandler handler)
            {
                return handler;
            }

            throw new InvalidOperationException(
                $"{service?.GetType().Name ?? "Service"} does not implement " +
                $"{nameof(IMirrorServerHandler)}.");
        }
    }
}
