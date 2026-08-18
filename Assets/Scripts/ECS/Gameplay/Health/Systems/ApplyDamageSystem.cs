using ECS.Gameplay.Damage.Components;
using ECS.Gameplay.Death.Components;
using ECS.Gameplay.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Health.Systems
{
    public class ApplyDamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageSignal, HealthComponent>
            .Exclude<DeathRequestedTag> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref HealthComponent health = ref filter.Get2(index);
                health.Value = Mathf.Max(
                    0f,
                    health.Value - filter.Get1(index).Value);

                ref EcsEntity entity = ref filter.GetEntity(index);
                entity.Get<HealthChangedSignal>();

                if (health.Value <= 0f)
                {
                    entity.Get<DeathRequestedTag>();
                }
            }
        }
    }
}
