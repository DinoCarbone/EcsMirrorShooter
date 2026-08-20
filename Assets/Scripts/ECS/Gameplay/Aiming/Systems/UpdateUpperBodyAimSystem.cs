using ECS.Common.Tags;
using ECS.Gameplay.Aiming.Components;
using ECS.Gameplay.Rotation.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Aiming.Systems
{
    public class UpdateUpperBodyAimSystem : IEcsRunSystem
    {
        private const float MinimumPitch = -30f;
        private const float MaximumPitch = 60f;

        private EcsFilter<
            PlayerTag,
            RotationInputComponent,
            UpperBodyAimComponent>
            .Exclude<PlayerControlDisabledTag> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref RotationInputComponent rotationInput = ref filter.Get2(index);
                ref UpperBodyAimComponent upperBodyAim = ref filter.Get3(index);
                Transform pivot = upperBodyAim.Pivot;
                float pitchDelta = rotationInput.Pitch;

                if (pivot == null || Mathf.Approximately(pitchDelta, 0f))
                {
                    continue;
                }

                float pitch = NormalizeAngle(pivot.localEulerAngles.x);
                pitch = Mathf.Clamp(
                    pitch - pitchDelta,
                    MinimumPitch,
                    MaximumPitch);

                pivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            }
        }

        private static float NormalizeAngle(float angle)
        {
            return angle > 180f ? angle - 360f : angle;
        }
    }
}
