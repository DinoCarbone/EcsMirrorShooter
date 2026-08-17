using ECS.Gameplay.Damage.Components;
using ECS.Gameplay.Health.Components;
using Leopotam.Ecs;

namespace ECS.Gameplay.Health.Systems
{
    public class ApplyDamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageSignal, HealthComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                ref HealthComponent health = ref filter.Get2(index);
                health.Value -= filter.Get1(index).Value;
                filter.GetEntity(index).Get<HealthChangedSignal>();
            }
        }
    }
}
