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
        private readonly IPlayerCameraService playerCameraService;

        private EcsFilter<PlayerTag, CameraTargetComponent, DestroyRequest> filter = null;

        public UnbindPlayerCameraSystem(IPlayerCameraService playerCameraService)
        {
            this.playerCameraService = playerCameraService;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                playerCameraService.ClearTarget();
                break;
            }
        }
    }
}
