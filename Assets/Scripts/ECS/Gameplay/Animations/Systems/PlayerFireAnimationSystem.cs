using ECS.Common.Animation.Components;
using ECS.Gameplay.Shooting.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Animations.Systems
{
    public class PlayerFireAnimationSystem : IEcsRunSystem
    {
        private static readonly int Fire = Animator.StringToHash("Fire");

        private EcsFilter<AnimatorComponent, SpawnBulletSignal> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref AnimatorComponent animator = ref filter.Get1(index);
                animator.Value?.SetTrigger(Fire);
            }
        }
    }
}
