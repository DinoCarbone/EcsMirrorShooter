using ECS.Common.Camera.Components;
using ECS.Common.Tags;
using ECS.Gameplay.Rotation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Camera.Systems
{
    public class RotatePlayerCameraSystem : IEcsRunSystem
    {
        private const float MinimumPitch = -30f;
        private const float MaximumPitch = 90f;

        private EcsFilter<
            PlayerTag,
            CameraTargetComponent,
            RotationInputComponent>
            .Exclude<PlayerControlDisabledTag> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref RotationInputComponent rotationInput = ref filter.Get3(index);
                float pitchDelta = rotationInput.Pitch;
                rotationInput.Pitch = 0f;

                Transform cameraTarget = filter.Get2(index).Value;
                if (cameraTarget == null || Mathf.Approximately(pitchDelta, 0f))
                {
                    continue;
                }

                float pitch = NormalizeAngle(cameraTarget.localEulerAngles.x);
                pitch = Mathf.Clamp(pitch - pitchDelta, MinimumPitch, MaximumPitch);
                cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            }
        }

        private static float NormalizeAngle(float angle)
        {
            return angle > 180f ? angle - 360f : angle;
        }
    }
}
