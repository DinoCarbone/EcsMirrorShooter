using ECS.Common.Animation.Components;
using ECS.Gameplay.Jump.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Animations.Systems
{
    public class PlayerJumpAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsGrounded =
            Animator.StringToHash("IsGrounded");

        private EcsFilter<AnimatorComponent, GroundCheckComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref AnimatorComponent animator = ref filter.Get1(index);
                GroundCheckComponent groundCheck = filter.Get2(index);

                animator.Value?.SetBool(IsGrounded, groundCheck.IsGrounded);
            }
        }
    }
}
