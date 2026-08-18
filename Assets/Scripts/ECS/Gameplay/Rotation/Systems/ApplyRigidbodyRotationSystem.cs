using ECS.Common.Tags;
using ECS.Gameplay.Movement.Components;
using ECS.Gameplay.Rotation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Rotation.Systems
{
    public class ApplyRigidbodyRotationSystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyComponent, RotationInputComponent>
            .Exclude<PlayerControlDisabledTag> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                Rigidbody rigidbody = filter.Get1(index).Value;
                ref RotationInputComponent rotationInput = ref filter.Get2(index);
                float yaw = rotationInput.Yaw;
                rotationInput.Yaw = 0f;

                if (rigidbody == null || Mathf.Approximately(yaw, 0f))
                {
                    continue;
                }

                Quaternion rotationDelta = Quaternion.AngleAxis(yaw, Vector3.up);
                rigidbody.MoveRotation(rotationDelta * rigidbody.rotation);
            }
        }
    }
}
