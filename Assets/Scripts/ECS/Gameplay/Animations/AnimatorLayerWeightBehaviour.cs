using UnityEngine;

namespace ECS.Gameplay.Animations
{
    public class AnimatorLayerWeightBehaviour : StateMachineBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float weight;

        private float initialWeight;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            initialWeight = animator.GetLayerWeight(layerIndex);

            UpdateLayerWeight(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            UpdateLayerWeight(animator, stateInfo, layerIndex);
        }

        private void UpdateLayerWeight(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (!animator.IsInTransition(layerIndex))
            {
                animator.SetLayerWeight(layerIndex, weight);
                return;
            }

            AnimatorStateInfo nextState =
                animator.GetNextAnimatorStateInfo(layerIndex);
            AnimatorStateInfo currentState =
                animator.GetCurrentAnimatorStateInfo(layerIndex);

            if (currentState.fullPathHash == nextState.fullPathHash)
            {
                animator.SetLayerWeight(layerIndex, weight);
                return;
            }

            if (nextState.fullPathHash != stateInfo.fullPathHash)
            {
                return;
            }

            float progress = animator
                .GetAnimatorTransitionInfo(layerIndex)
                .normalizedTime;

            animator.SetLayerWeight(
                layerIndex,
                Mathf.Lerp(initialWeight, weight, Mathf.Clamp01(progress)));
        }
    }
}
