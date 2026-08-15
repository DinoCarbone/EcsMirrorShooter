using ECS.Gameplay.Jump.Components;
using ECS.Gameplay.Movement.Components;
using ECS.Common.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Jump.Systems
{
    public class PlayertInputJumpSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, JumpInputComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref JumpInputComponent jumpInput = ref filter.Get2(index);
                if (Input.GetKeyDown(KeyCode.Space)) jumpInput.Value = true;
                else jumpInput.Value = false;
            }
        }
    }
}
