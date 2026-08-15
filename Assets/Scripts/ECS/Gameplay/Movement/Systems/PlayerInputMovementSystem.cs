using ECS.Gameplay.Movement.Components;
using ECS.Common.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class PlayertInputMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MoveInputComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                Vector2 direction = new Vector2(h, v).normalized;

                ref var input = ref filter.Get2(index);
                input.Value = direction;
            }
        }
    }
}
