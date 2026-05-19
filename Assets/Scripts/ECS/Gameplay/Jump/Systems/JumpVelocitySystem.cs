using ECS.Gameplay.Jump.Components;
using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Jump.Systems
{
    public class JumpVelocitySystem : IEcsRunSystem
    {
        private EcsFilter<JumpInputComponent, JumpForceComponent, JumpVelocityComponent, GroundCheckComponent> filter = null;
        public void Run()
        {
            foreach (var index in filter)
            {
                var jumpInput = filter.Get1(index);
                var jumpForce = filter.Get2(index);
                var groundCheck = filter.Get4(index);
                
                ref var jumpVelocity = ref filter.Get3(index);

                if (jumpInput.Value && groundCheck.IsGrounded)
                {
                    jumpVelocity.Value = Vector3.up * jumpForce.Value;
                }
                else jumpVelocity.Value = default;
            }
        }
    }
}