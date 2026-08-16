using ECS.Common.Tags;
using ECS.Common.Camera.Components;
using ECS.Common.Camera.Interfaces;
using Leopotam.Ecs;

namespace ECS.Common.Camera.Systems
{
    public class BindPlayerCameraSystem : IEcsRunSystem
    {
        private readonly IPlayerCameraSystem playerCameraSystem;

        private EcsFilter<PlayerTag, CameraTargetComponent>
            .Exclude<CameraTargetAssignedTag> filter = null;

        public BindPlayerCameraSystem(IPlayerCameraSystem playerCameraSystem)
        {
            this.playerCameraSystem = playerCameraSystem;
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

                playerCameraSystem.SetTarget(cameraTarget.Value);
                playerCameraSystem.EnableRotation();
                filter.GetEntity(index).Get<CameraTargetAssignedTag>();
                break;
            }
        }
    }
}
