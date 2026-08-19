namespace ECS.Common.Animation.Interfaces
{
    public interface IAnimator
    {
        void SetFloat(int nameHash, float value);
        void SetFloat(int nameHash, float value, float dampTime, float deltaTime);
        void SetBool(int nameHash, bool value);
        void SetInteger(int nameHash, int value);
        void SetTrigger(int nameHash);
        void ResetTrigger(int nameHash);
    }
}
