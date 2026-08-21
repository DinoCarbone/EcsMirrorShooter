using ECS.Gameplay.Health.MonoLinks;

namespace ECS.Gameplay.Health.Interfaces
{
    public interface IHealthBarFactory
    {
        HealthBarMonoLink Create();
    }
}
