using UnityEngine;

namespace ECS.Gameplay.Camera.Interfaces
{
    public interface IPlayerCameraBinder
    {
        void SetTarget(Transform target);
    }
}
