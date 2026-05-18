using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class GroundCheckSystem : IEcsRunSystem
    {
        private EcsFilter<GroundCheckComponent> filter = null;

        public void Run()
        {
            foreach (var index in filter)
            {
                ref var groundCheck = ref filter.Get1(index);

                if (groundCheck.GroundCheckPoint == null)
                {
                    groundCheck.IsGrounded = false;
                    continue;
                }

                float rayLength = 0.1f;
                var rayStart = groundCheck.GroundCheckPoint.position;
                var rayDirection = Vector3.down;

                // Выполняем рейкаст
                groundCheck.IsGrounded = Physics.Raycast(rayStart, rayDirection, rayLength);
                Debug.Log(groundCheck.IsGrounded);
            }
        }
    }
}