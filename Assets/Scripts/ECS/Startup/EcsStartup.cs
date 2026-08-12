using System;
using ECS.Gameplay.Jump.Systems;
using ECS.Gameplay.Movement.Systems;
using Leopotam.Ecs;
using Zenject;

namespace ECS.Startup
{
    public class EcsStartup : IEcsWorldProvider, IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly EcsWorld world = new();
        private EcsSystems systems;
        private EcsSystems fixedSystems;

        public EcsWorld World => world;

        public void Initialize()
        {
            systems = new EcsSystems(world)
                .Add(new PlayertInputMovementSystem())
                .Add(new MoveVelocitySystem())
                .Add(new PlayertInputJumpSystem())
                .Add(new JumpVelocitySystem())
                .Add(new JumpForceSystem())
                .Add(new GroundCheckSystem());
            systems.Init();

            fixedSystems = new EcsSystems(world)
                .Add(new UpdateRigidbodyPositionSystem());
            fixedSystems.Init();
        }

        public void Tick()
        {
            systems.Run();
        }

        public void FixedTick()
        {
            fixedSystems.Run();
        }

        public void Dispose()
        {
            fixedSystems?.Destroy();
            fixedSystems = null;

            systems?.Destroy();
            systems = null;

            world.Destroy();
        }
    }
}
