using ECS.Common.Lifecycle.Components;
using ECS.Common.Tags;
using ECS.Common.Camera.Components;
using ECS.Common.Camera.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Camera.Systems
{
    public class UnbindPlayerCameraSystem : IEcsRunSystem
    {
        private readonly IPlayerCameraSystem playerCameraSystem;

        private EcsFilter<PlayerTag, CameraTargetComponent, DestroyRequest> filter = null;

        public UnbindPlayerCameraSystem(IPlayerCameraSystem playerCameraSystem)
        {
            this.playerCameraSystem = playerCameraSystem;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                Debug.Log("Unbinding camera");
                playerCameraSystem.ClearTarget();
                playerCameraSystem.DisableRotation();
                break;
            }
        }
    }
}
