using UnityEngine;

namespace ECS.Common.Camera.Interfaces
{
    public interface IPlayerCameraSystem
    {
        void SetTarget(Transform target);
        void ClearTarget();
        void EnableRotation();
        void DisableRotation();
    }
}
