using ECS.Gameplay.Movement.Components;
using ECS.Gameplay.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Movement.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MoveInputComponent> _playerFilter = null;

        public void Run()
        {
            foreach (int index in _playerFilter)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                Vector2 direction = new Vector2(h, v).normalized;

                ref var input = ref _playerFilter.Get2(index);
                input.Value = direction;
            }
        }
    }
}