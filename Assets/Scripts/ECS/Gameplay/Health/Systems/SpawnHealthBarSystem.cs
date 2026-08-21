using System;
using ECS.Common.Lifecycle.Components;
using ECS.Common.Tags;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using ECS.Gameplay.Health.MonoLinks;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Health.Systems
{
    public class SpawnHealthBarSystem : IEcsRunSystem
    {
        private readonly IHealthBarFactory healthBarFactory;

        private EcsFilter<PlayerTag, HealthComponent>
            .Exclude<HealthBarComponent> filter = null;

        public SpawnHealthBarSystem(IHealthBarFactory healthBarFactory)
        {
            this.healthBarFactory = healthBarFactory ??
                throw new ArgumentNullException(nameof(healthBarFactory));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                ref EcsEntity entity = ref filter.GetEntity(index);
                if (entity.Has<DestroyRequest>())
                {
                    continue;
                }

                HealthComponent health = filter.Get2(index);
                HealthBarMonoLink healthBar = healthBarFactory.Create();
                healthBar.Make(ref entity);

                float normalizedValue = health.MaxValue > 0f
                    ? Mathf.Clamp01(health.Value / health.MaxValue)
                    : 0f;

                healthBar.SetValue(normalizedValue);
            }
        }
    }
}
