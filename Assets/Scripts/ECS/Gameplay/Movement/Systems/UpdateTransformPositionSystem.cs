using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class UpdateTransformPositionSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, VelocityComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                Transform transform = filter.Get1(i).Value;
                Vector3 velocity = filter.Get2(i).Value;

                transform.position += velocity;
            }
        }
    }
}