using UnityEngine;

namespace ECS.Common.Camera.Interfaces
{
    public interface IPlayerCameraService
    {
        void SetTarget(Transform target);
        void ClearTarget();
    }
}
