using System;
using ECS.Common.Lifecycle.Interfaces;
using ECS.Common.Lifecycle.Systems;
using ECS.Common.Camera.Interfaces;
using ECS.Common.Camera.Systems;
using ECS.Common.Cursor.Interfaces;
using ECS.Common.Cursor.Systems;
using ECS.Common.Menu.Interfaces;
using ECS.Common.Menu.Systems;
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
        private readonly IBulletSpawner bulletSpawner;
        private readonly IEntityDestroyer entityDestroyer;
        private readonly IPlayerCameraSystem playerCameraSystem;
        private readonly IPlayerCursorSystem playerCursorSystem;
        private readonly IPlayerMenuService playerMenuService;
        private EcsSystems systems;
        private EcsSystems fixedSystems;

        public EcsStartup(
            IEcsWorldProvider worldProvider,
            IBulletSpawner bulletSpawner,
            IEntityDestroyer entityDestroyer,
            IPlayerCameraSystem playerCameraSystem,
            IPlayerCursorSystem playerCursorSystem,
            IPlayerMenuService playerMenuService)
        {
            this.worldProvider = worldProvider;
            this.bulletSpawner = bulletSpawner;
            this.entityDestroyer = entityDestroyer;
            this.playerCameraSystem = playerCameraSystem;
            this.playerCursorSystem = playerCursorSystem;
            this.playerMenuService = playerMenuService;
        }

        public void Initialize()
        {
            systems = new EcsSystems(worldProvider.World)
                .Add(new BindPlayerCameraSystem(playerCameraSystem))
                .Add(new HidePlayerCursorSystem(playerCursorSystem))
                .Add(new TogglePlayerMenuSystem(playerMenuService))
                .Add(new PlayertInputMovementSystem())
                .Add(new PlayertInputJumpSystem())
                .Add(new PlayerShootInputSystem())
                .Add(new ResetDisabledPlayerInputSystem(
                    playerCameraSystem,
                    playerCursorSystem))
                .Add(new MoveVelocitySystem())
                .Add(new SpawnBulletSystem(bulletSpawner))
                .Add(new JumpVelocitySystem())
                .Add(new JumpForceSystem())
                .Add(new GroundCheckSystem())
                .Add(new LifetimeSystem())
                .Add(new DestroyPlayerMenuSystem(playerMenuService))
                .Add(new DestroyGameObjectSystem(entityDestroyer))
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
