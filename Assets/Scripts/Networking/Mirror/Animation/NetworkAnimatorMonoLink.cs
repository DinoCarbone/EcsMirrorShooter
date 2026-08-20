using ECS.Common.Animation.Components;
using ECS.Common.Animation.Interfaces;
using ECS.Common.MonoLinksBase;
using Leopotam.Ecs;
using global::Mirror;
using UnityEngine;

namespace Networking.Mirror.Animation
{
    public class NetworkAnimatorMonoLink : MonoLink<AnimatorComponent>, IAnimator
    {
        [SerializeField] private NetworkAnimator networkAnimator;

        public override void Make(ref EcsEntity entity)
        {
            Value.Value = this;
            base.Make(ref entity);
        }

        public void SetFloat(int nameHash, float value)
        {
            networkAnimator.animator.SetFloat(nameHash, value);
        }

        public void SetFloat(int nameHash, float value, float dampTime, float deltaTime)
        {
            networkAnimator.animator.SetFloat(nameHash, value, dampTime, deltaTime);
        }

        public void SetBool(int nameHash, bool value)
        {
            networkAnimator.animator.SetBool(nameHash, value);
        }

        public void SetInteger(int nameHash, int value)
        {
            networkAnimator.animator.SetInteger(nameHash, value);
        }

        public void SetTrigger(int nameHash)
        {
            networkAnimator.SetTrigger(nameHash);
        }

        public void ResetTrigger(int nameHash)
        {
            networkAnimator.ResetTrigger(nameHash);
        }
    }
}
