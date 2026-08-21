using System;
using ECS.Gameplay.Health.Interfaces;
using ECS.Gameplay.Health.MonoLinks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace ECS.Gameplay.Health.Services
{
    public class HealthBarFactory : IHealthBarFactory, IHealthBarDestroyer
    {
        private readonly GameObject healthBarPrefab;
        private readonly Canvas healthBarCanvas;
        private readonly IInstantiator instantiator;

        private GameObject instance;

        public HealthBarFactory(
            GameObject healthBarPrefab,
            Canvas healthBarCanvas,
            IInstantiator instantiator)
        {
            this.healthBarPrefab = healthBarPrefab ??
                throw new ArgumentNullException(nameof(healthBarPrefab));
            this.healthBarCanvas = healthBarCanvas ??
                throw new ArgumentNullException(nameof(healthBarCanvas));
            this.instantiator = instantiator ??
                throw new ArgumentNullException(nameof(instantiator));
        }

        public HealthBarMonoLink Create()
        {
            if (instance != null)
            {
                HealthBarMonoLink existingLink =
                    instance.GetComponentInChildren<HealthBarMonoLink>(true);

                if (existingLink != null)
                {
                    return existingLink;
                }

                Destroy();
            }

            instance = instantiator.InstantiatePrefab(
                healthBarPrefab,
                healthBarCanvas.transform);

            HealthBarMonoLink healthBar =
                instance.GetComponentInChildren<HealthBarMonoLink>(true);

            if (healthBar == null)
            {
                Destroy();
                throw new InvalidOperationException(
                    $"Prefab '{healthBarPrefab.name}' does not contain " +
                    $"{nameof(HealthBarMonoLink)}.");
            }

            return healthBar;
        }

        public void Destroy()
        {
            if (instance == null)
            {
                return;
            }

            Object.Destroy(instance);
            instance = null;
        }
    }
}
