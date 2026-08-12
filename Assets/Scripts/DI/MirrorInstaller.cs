using global::Mirror;
using Networking.Mirror;
using UnityEngine;
using Zenject;

namespace DI
{
    public sealed class MirrorInstaller : MonoInstaller
    {
        [SerializeField] private ZenjectNetworkManager networkManager;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ZenjectNetworkManager>()
                .FromNewComponentOnNewPrefab(networkManager).AsSingle().NonLazy();
            // Container.Bind<ZenjectNetworkManager>()
            //     .FromComponentInNewPrefab(networkManager)
            //     .AsSingle().NonLazy();
            //
            // Container.Bind<NetworkManager>()
            //     .To<ZenjectNetworkManager>()
            //     .FromResolve();
        }
    }
}
