using ECS.Common.MonoLinksBase;
using ECS.Gameplay.Health.Components;
using Leopotam.Ecs;

namespace ECS.Gameplay.Health.MonoLinks
{
    public class HealthMonoLink : MonoLink<HealthComponent>
    {
        public override void Make(ref EcsEntity entity)
        {
            Value.Value = Value.MaxValue;
            base.Make(ref entity);
        }
    }
}
