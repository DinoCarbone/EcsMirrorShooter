using ECS.Common.Tags;
using ECS.Gameplay.Camera.Components;
using ECS.Gameplay.Camera.Interfaces;
using Leopotam.Ecs;

namespace ECS.Gameplay.Camera.Systems
{
    public class BindPlayerCameraSystem : IEcsRunSystem
    {
        private readonly IPlayerCameraBinder cameraBinder;

        private EcsFilter<PlayerTag, CameraTargetComponent>
            .Exclude<CameraTargetAssignedTag> filter = null;

        public BindPlayerCameraSystem(IPlayerCameraBinder cameraBinder)
        {
            this.cameraBinder = cameraBinder;
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

                cameraBinder.SetTarget(cameraTarget.Value);
                filter.GetEntity(index).Get<CameraTargetAssignedTag>();
                break;
            }
        }
    }
}
