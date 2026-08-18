using ECS.Common.Tags;
using ECS.Gameplay.Rotation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Rotation.Systems
{
    public class PlayerRotationInputSystem : IEcsRunSystem
    {
        private const string MouseXAxis = "Mouse X";
        private const string MouseYAxis = "Mouse Y";
        private const float RotationSpeed = 350f;

        private EcsFilter<PlayerTag>
            .Exclude<PlayerControlDisabledTag> filter = null;

        public void Run()
        {
            float yaw = Input.GetAxisRaw(MouseXAxis) * RotationSpeed * Time.deltaTime;
            float pitch = Input.GetAxisRaw(MouseYAxis) * RotationSpeed * Time.deltaTime;

            foreach (int index in filter)
            {
                ref EcsEntity entity = ref filter.GetEntity(index);
                ref RotationInputComponent rotationInput = ref entity
                    .Get<RotationInputComponent>();

                rotationInput.Yaw += yaw;
                rotationInput.Pitch += pitch;
            }
        }
    }
}
