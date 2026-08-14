using System;
using ECS.Gameplay.Jump.Systems;
using ECS.Gameplay.Movement.Systems;
using ECS.Gameplay.Shooting.Components;
using ECS.Gameplay.Shooting.Interfaces;
using ECS.Gameplay.Shooting.Systems;
using Leopotam.Ecs;
using Zenject;

namespace ECS.Startup
{
    public class EcsStartup : IInitializable, ITickable, IFixedTickable, IDisposable
    {
        private readonly IEcsWorldProvider worldProvider;
        private IBulletSpawner bulletSpawner;
        private EcsSystems systems;
        private EcsSystems fixedSystems;

        public EcsStartup(IEcsWorldProvider worldProvider)
        {
            this.worldProvider = worldProvider;
        }

        [Inject]
        public void Construct(IBulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner;
        }

        public void Initialize()
        {
            systems = new EcsSystems(worldProvider.World)
                .Add(new PlayertInputMovementSystem())
                .Add(new MoveVelocitySystem())
                .Add(new PlayertInputJumpSystem())
                .Add(new PlayerShootInputSystem())
                .Add(new SpawnBulletSystem(bulletSpawner))
                .Add(new JumpVelocitySystem())
                .Add(new JumpForceSystem())
                .Add(new GroundCheckSystem())
                .OneFrame<SpawnBulletSignal>();
            systems.Init();

            fixedSystems = new EcsSystems(worldProvider.World)
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
        }
    }
}
