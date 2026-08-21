using System.Collections.Generic;
using Mock;
using UnityEngine;
using Zenject;

namespace DI
{
    public class AutomaticSpawnInstaller : MonoInstaller
    {
        [SerializeField] private List<AutomaticSpawnEntry> entries = new();

        public override void InstallBindings()
        {
            Container.Bind<IReadOnlyList<AutomaticSpawnEntry>>().FromInstance(entries);
            Container.BindInterfacesAndSelfTo<AutomaticSpawnSystem>().AsSingle().NonLazy();
        }
    }
}
