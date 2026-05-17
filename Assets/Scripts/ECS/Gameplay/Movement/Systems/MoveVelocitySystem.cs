using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class MoveVelocitySystem : IEcsRunSystem
    {
        private EcsFilter<VelocityComponent, MoveInputComponent, MoveSpeedComponent> filter = null;
        
        public void Run()
        {
            foreach (var index in filter)
            {
                float speed = filter.Get3(index).Value;
                Vector2 input = filter.Get2(index).Value;
                Vector3 direction = new Vector3(input.x, 0, input.y);
                
                ref var moveComponent = ref filter.Get1(index);
                moveComponent.Value = direction * speed * Time.deltaTime;
            }
        }
    }
}