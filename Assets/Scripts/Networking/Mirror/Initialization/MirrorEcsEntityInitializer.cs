using ECS.Startup;
using global::Mirror;
using Leopotam.Ecs;
using UnityComponents.MonoLinksBase;
using UnityEngine;
using Zenject;

namespace Networking.Mirror.Initialization
{
    [RequireComponent(typeof(NetworkIdentity), typeof(MonoEntity))]
    public class MirrorEcsEntityInitializer : NetworkBehaviour
    {
        private MonoEntity monoEntity;

        private IEcsWorldProvider worldProvider;
        private bool initialized;

        [Inject]
        public void Construct(IEcsWorldProvider ecsWorldProvider)
        {
            monoEntity = GetComponent<MonoEntity>();
            if (monoEntity == null) Debug.LogError("MirrorEcsEntityInitializer requires MonoEntity component");
            worldProvider = ecsWorldProvider;
        }

        public override void OnStartServer()
        {
            InitializeEntity();
        }

        public override void OnStartClient()
        {
            InitializeEntity();
        }

        private void InitializeEntity()
        {
            if (initialized || !isLocalPlayer)
            {
                return;
            }

            EcsEntity entity = worldProvider.World.NewEntity();
            monoEntity.Make(ref entity);
            initialized = true;
        }
    }
}
