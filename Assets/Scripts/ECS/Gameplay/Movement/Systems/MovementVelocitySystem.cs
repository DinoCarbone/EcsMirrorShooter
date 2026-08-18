using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class MovementVelocitySystem : IEcsRunSystem
    {
        private EcsFilter<
            MovementVelocityComponent,
            MoveInputComponent,
            MoveSpeedComponent,
            TransformComponent> filter = null;
        
        public void Run()
        {
            foreach (var index in filter)
            {
                float speed = filter.Get3(index).Value;
                Vector2 input = filter.Get2(index).Value;
                Transform transform = filter.Get4(index).Value;
                Vector3 localDirection = new Vector3(input.x, 0f, input.y);
                Vector3 direction = transform != null
                    ? transform.TransformDirection(localDirection)
                    : Vector3.zero;
                
                ref var moveComponent = ref filter.Get1(index);
                moveComponent.Value = direction * speed;
            }
        }
    }
}
