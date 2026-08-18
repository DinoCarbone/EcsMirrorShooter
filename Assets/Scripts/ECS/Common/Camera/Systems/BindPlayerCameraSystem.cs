using ECS.Common.Tags;
using ECS.Common.Camera.Components;
using ECS.Common.Camera.Interfaces;
using Leopotam.Ecs;

namespace ECS.Common.Camera.Systems
{
    public class BindPlayerCameraSystem : IEcsRunSystem
    {
        private readonly IPlayerCameraService playerCameraService;

        private EcsFilter<PlayerTag, CameraTargetComponent>
            .Exclude<CameraTargetAssignedTag> filter = null;

        public BindPlayerCameraSystem(IPlayerCameraService playerCameraService)
        {
            this.playerCameraService = playerCameraService;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                ref CameraTargetComponent cameraTarget = ref filter.Get2(index);
                if (cameraTarget.Value == null)
                {
                    continue;
                }

                playerCameraService.SetTarget(cameraTarget.Value);
                filter.GetEntity(index).Get<CameraTargetAssignedTag>();
                break;
            }
        }
    }
}
