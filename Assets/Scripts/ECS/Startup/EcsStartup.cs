using System;
using ECS.Common.Lifecycle.Interfaces;
using ECS.Common.Lifecycle.Systems;
using ECS.Common.Camera.Interfaces;
using ECS.Common.Camera.Systems;
using ECS.Common.Cursor.Interfaces;
using ECS.Common.Cursor.Systems;
using ECS.Common.Menu.Interfaces;
using ECS.Common.Menu.Systems;
using ECS.Common.Collision.Components;
using ECS.Gameplay.Damage.Components;
using ECS.Gameplay.Damage.Interfaces;
using ECS.Gameplay.Damage.Systems;
using ECS.Gameplay.Death.Interfaces;
using ECS.Gameplay.Death.Systems;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using ECS.Gameplay.Health.Systems;
using ECS.Gameplay.Jump.Systems;
using ECS.Gameplay.Movement.Systems;
using ECS.Gameplay.Rotation.Systems;
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
        private readonly IDamageService damageService;
        private readonly IPlayerDeathService playerDeathService;
        private readonly IUpdateHealthBarService updateHealthBarService;
        private readonly IPlayerCameraService playerCameraService;
        private readonly IPlayerCursorSystem playerCursorSystem;
        private readonly IPlayerMenuService playerMenuService;
        private EcsSystems systems;
        private EcsSystems fixedSystems;

        public EcsStartup(
            IEcsWorldProvider worldProvider,
            IBulletSpawner bulletSpawner,
            IEntityDestroyer entityDestroyer,
            IDamageService damageService,
            IPlayerDeathService playerDeathService,
            IUpdateHealthBarService updateHealthBarService,
            IPlayerCameraService playerCameraService,
            IPlayerCursorSystem playerCursorSystem,
            IPlayerMenuService playerMenuService)
        {
            this.worldProvider = worldProvider;
            this.bulletSpawner = bulletSpawner;
            this.entityDestroyer = entityDestroyer;
            this.damageService = damageService;
            this.playerDeathService = playerDeathService;
            this.updateHealthBarService = updateHealthBarService;
            this.playerCameraService = playerCameraService;
            this.playerCursorSystem = playerCursorSystem;
            this.playerMenuService = playerMenuService;
        }

        public void Initialize()
        {
            systems = new EcsSystems(worldProvider.World)
                .Add(new BindPlayerCameraSystem(playerCameraService))
                .Add(new HidePlayerCursorSystem(playerCursorSystem))
                .Add(new TogglePlayerMenuSystem(playerMenuService))
                .Add(new PlayertInputMovementSystem())
                .Add(new PlayertInputJumpSystem())
                .Add(new PlayerShootInputSystem())
                .Add(new ResetDisabledPlayerInputSystem(playerCursorSystem))
                .Add(new PlayerRotationInputSystem())
                .Add(new RotatePlayerCameraSystem())
                .Add(new MovementVelocitySystem())
                .Add(new SpawnBulletSystem(bulletSpawner))
                .Add(new ApplyCollisionDamageSystem(damageService))
                .Add(new DestroyBulletOnCollisionSystem())
                .Add(new ApplyDamageSystem())
                .Add(new PlayerDeathSystem(playerDeathService))
                .Add(new UpdateHealthBarSystem(updateHealthBarService))
                .Add(new LifetimeSystem())
                .Add(new DestroyPlayerMenuSystem(playerMenuService))
                .Add(new DestroyGameObjectSystem(entityDestroyer))
                .OneFrame<SpawnBulletSignal>()
                .OneFrame<DamageSignal>()
                .OneFrame<HealthChangedSignal>()
                .OneFrame<CollisionSignal>();
            systems.Init();

            fixedSystems = new EcsSystems(worldProvider.World)
                .Add(new ApplyRigidbodyRotationSystem())
                .Add(new GroundCheckSystem())
                .Add(new JumpVelocitySystem())
                .Add(new ApplyRigidbodyVelocitySystem())
                .Add(new JumpForceSystem());
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
