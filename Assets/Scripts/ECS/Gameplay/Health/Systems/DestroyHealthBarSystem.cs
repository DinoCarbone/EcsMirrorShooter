using System;
using ECS.Common.Lifecycle.Components;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using Leopotam.Ecs;

namespace ECS.Gameplay.Health.Systems
{
    public class DestroyHealthBarSystem : IEcsRunSystem
    {
        private readonly IHealthBarDestroyer healthBarDestroyer;

        private EcsFilter<HealthBarComponent, DestroyRequest> filter = null;

        public DestroyHealthBarSystem(IHealthBarDestroyer healthBarDestroyer)
        {
            this.healthBarDestroyer = healthBarDestroyer ??
                throw new ArgumentNullException(nameof(healthBarDestroyer));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                ref EcsEntity entity = ref filter.GetEntity(index);
                healthBarDestroyer.Destroy();
                entity.Del<HealthBarComponent>();
            }
        }
    }
}
