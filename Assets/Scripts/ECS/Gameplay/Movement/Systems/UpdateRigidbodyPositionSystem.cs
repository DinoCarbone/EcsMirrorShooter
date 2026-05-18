using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class UpdateRigidbodyPositionSystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyComponent, MovementVelocityComponent> filter = null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var rigidbody = ref filter.Get1(entity);
                ref var velocity = ref filter.Get2(entity);

                Vector3 newPosition = rigidbody.Value.position + velocity.Value * Time.fixedDeltaTime;

                rigidbody.Value.MovePosition(newPosition);
            }
        }
    }
}