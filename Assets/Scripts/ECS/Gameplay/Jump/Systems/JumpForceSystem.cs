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

                if (jumpVelocity.Value == default) continue;
                rigidbody.Value.AddForce(jumpVelocity.Value, ForceMode.Impulse);
            }
        }
    }
}