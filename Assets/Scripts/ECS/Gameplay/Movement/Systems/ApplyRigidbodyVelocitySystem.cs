using ECS.Common.Tags;
using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class ApplyRigidbodyVelocitySystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyComponent, MovementVelocityComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                Rigidbody rigidbody = filter.Get1(index).Value;
                if (rigidbody == null)
                {
                    continue;
                }

                Vector3 velocity = filter.Get2(index).Value;
                if (filter.GetEntity(index).Has<PlayerTag>())
                {
                    velocity.y = rigidbody.linearVelocity.y;
                }

                rigidbody.linearVelocity = velocity;
            }
        }
    }
}
