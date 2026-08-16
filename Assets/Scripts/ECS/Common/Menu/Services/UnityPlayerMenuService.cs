using System;
using ECS.Common.Menu.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ECS.Common.Menu.Services
{
    public class UnityPlayerMenuService : IPlayerMenuService, IDisposable
    {
        private readonly GameObject menuPrefab;
        private readonly Canvas menuCanvas;
        private readonly IInstantiator instantiator;

        private GameObject menuInstance;

        public UnityPlayerMenuService(
            GameObject menuPrefab,
            Canvas menuCanvas,
            IInstantiator instantiator)
        {
            this.menuPrefab = menuPrefab ??
                throw new ArgumentNullException(nameof(menuPrefab));
            this.menuCanvas = menuCanvas ??
                throw new ArgumentNullException(nameof(menuCanvas));
            this.instantiator = instantiator ??
                throw new ArgumentNullException(nameof(instantiator));
        }

        public bool IsOpen => menuInstance != null;

        public void Open()
        {
            if (IsOpen)
            {
                return;
            }

            menuInstance = instantiator.InstantiatePrefab(
                menuPrefab,
                menuCanvas.transform);
        }

        public void Close()
        {
            if (menuInstance != null)
            {
                Object.Destroy(menuInstance);
                menuInstance = null;
            }
        }

        public void Dispose()
        {
            Close();
        }
    }
}
