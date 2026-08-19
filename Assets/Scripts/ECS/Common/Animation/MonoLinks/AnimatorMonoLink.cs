using ECS.Common.Animation.Components;
using ECS.Common.Animation.Interfaces;
using ECS.Common.MonoLinksBase;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Animation.MonoLinks
{
    public class AnimatorMonoLink : MonoLink<AnimatorComponent>, IAnimator
    {
        [SerializeField] private Animator animator;

        public override void Make(ref EcsEntity entity)
        {
            Value.Value = this;
            base.Make(ref entity);
        }

        public void SetFloat(int nameHash, float value)
        {
            animator.SetFloat(nameHash, value);
        }

        public void SetFloat(int nameHash, float value, float dampTime, float deltaTime)
        {
            animator.SetFloat(nameHash, value, dampTime, deltaTime);
        }

        public void SetBool(int nameHash, bool value)
        {
            animator.SetBool(nameHash, value);
        }

        public void SetInteger(int nameHash, int value)
        {
            animator.SetInteger(nameHash, value);
        }

        public void SetTrigger(int nameHash)
        {
            animator.SetTrigger(nameHash);
        }

        public void ResetTrigger(int nameHash)
        {
            animator.ResetTrigger(nameHash);
        }
    }
}
