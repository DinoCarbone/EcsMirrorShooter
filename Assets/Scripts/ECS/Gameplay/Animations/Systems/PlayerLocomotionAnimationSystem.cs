using ECS.Common.Animation.Components;
using ECS.Gameplay.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Animations.Systems
{
    public class PlayerLocomotionAnimationSystem : IEcsRunSystem
    {
        private const float DampTime = 0.1f;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

        private EcsFilter<AnimatorComponent, MoveInputComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref AnimatorComponent animator = ref filter.Get1(index);
                MoveInputComponent input = filter.Get2(index);

                animator.Value?.SetFloat(MoveX, input.Value.x, DampTime, Time.deltaTime);
                animator.Value?.SetFloat(MoveY, input.Value.y, DampTime, Time.deltaTime);
            }
        }
    }
}
