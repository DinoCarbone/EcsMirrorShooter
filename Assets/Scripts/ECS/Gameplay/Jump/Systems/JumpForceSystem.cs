using ECS.Gameplay.Jump.Components;
using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Jump.Systems
{
    public class JumpForceSystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyComponent, JumpVelocityComponent> filter = null;

        public void Run()
        {
            foreach (var index in filter)
            {
                ref var rigidbody = ref filter.Get1(index);
                ref var jumpVelocity = ref filter.Get2(index);
                Vector3 force = jumpVelocity.Value;
                jumpVelocity.Value = default;

                if (rigidbody.Value == null || force == default)
                {
                    continue;
                }

                rigidbody.Value.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
