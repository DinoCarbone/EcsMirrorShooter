using ECS.Common.Collision.Components;
using ECS.Common.Lifecycle.Components;
using ECS.Common.Tags;
using Leopotam.Ecs;

namespace ECS.Gameplay.Shooting.Systems
{
    public class DestroyBulletOnCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<CollisionSignal, BulletTag>
            .Exclude<DestroyRequest> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                filter.GetEntity(index).Get<DestroyRequest>();
            }
        }
    }
}
