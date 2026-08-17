using System;
using ECS.Common.Lifecycle.Components;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Health.Systems
{
    public class UpdateHealthBarSystem : IEcsRunSystem
    {
        private readonly IUpdateHealthBarService updateHealthBarService;
        private EcsFilter<
            HealthChangedSignal,
            HealthComponent,
            HealthBarComponent,
            GameObjectComponent> filter = null;

        public UpdateHealthBarSystem(IUpdateHealthBarService updateHealthBarService)
        {
            this.updateHealthBarService = updateHealthBarService ??
                throw new ArgumentNullException(nameof(updateHealthBarService));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                HealthComponent health = filter.Get2(index);
                float normalizedValue = health.MaxValue > 0f
                    ? Mathf.Clamp01(health.Value / health.MaxValue)
                    : 0f;
                
                updateHealthBarService.SetValue(
                    filter.Get4(index).Value,
                    filter.Get3(index).Value,
                    normalizedValue);
            }
        }
    }
}
