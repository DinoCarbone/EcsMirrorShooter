using ECS.Common.Lifecycle.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Lifecycle.Systems
{
    public class LifetimeSystem : IEcsRunSystem
    {
        private EcsFilter<LifetimeComponent>.Exclude<DestroyRequest> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref LifetimeComponent lifetime = ref filter.Get1(index);
                lifetime.RemainingSeconds -= Time.deltaTime;

                if (lifetime.RemainingSeconds <= 0f)
                {
                    filter.GetEntity(index).Get<DestroyRequest>();
                }
            }
        }
    }
}
