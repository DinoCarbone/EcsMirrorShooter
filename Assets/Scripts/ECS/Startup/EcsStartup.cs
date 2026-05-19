using ECS.Gameplay.Jump.Systems;
using ECS.Gameplay.Movement.Systems;
using Leopotam.Ecs;
using UnityComponents.MonoLinksBase;
using UnityEngine;

namespace ECS.Startup
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private EcsWorld world;
        private EcsSystems systems;
        private EcsSystems fixedSystems;

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            systems
                .Add(new PlayertInputMovementSystem())
                .Add(new MoveVelocitySystem())
                .Add(new PlayertInputJumpSystem())
                .Add(new JumpVelocitySystem())
                .Add(new JumpForceSystem())
                .Add(new GroundCheckSystem());
                

            systems.Init();

            fixedSystems = new EcsSystems(world);
            fixedSystems.Add(new UpdateRigidbodyPositionSystem());
            
            fixedSystems.Init();

            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab is not assigned in the inspector!");
                return;
            }

            var playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            var monoEntity = playerInstance.GetComponent<MonoEntity>();
            if (monoEntity == null) 
                return;
            EcsEntity ecsEntity = world.NewEntity();
            monoEntity.Make(ref ecsEntity);
        }

        private void Update()
        {
            if (systems != null)
            {
                systems.Run();
            }
        }
        private void FixedUpdate()
        {
            if (fixedSystems != null)
            {
                fixedSystems.Run();
            }
        }

        private void OnDestroy()
        {
            if (systems != null)
            {
                systems.Destroy();
                systems = null;
            }

            if (world != null)
            {
                world.Destroy();
                world = null;
            }
        }
    }
}