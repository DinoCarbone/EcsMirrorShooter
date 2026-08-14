using ECS.Startup;
using global::Mirror;
using UnityEngine;
using Zenject;

namespace Networking.Mirror.Integration
{
    public class ZenjectNetworkManager : NetworkManager
    {
        private IInstantiator instantiator;
        private IEcsEntityFactory ecsEntityFactory;

        [Inject]
        public void Construct(IInstantiator prefabInstantiator, IEcsEntityFactory ecsEntityFactory)
        {
            instantiator = prefabInstantiator;
            this.ecsEntityFactory = ecsEntityFactory;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient connection)
        {
            Transform startPosition = GetStartPosition();
            Vector3 position = startPosition != null ? startPosition.position : Vector3.zero;
            Quaternion rotation = startPosition != null ? startPosition.rotation : Quaternion.identity;
            
            GameObject player = instantiator.InstantiatePrefab(playerPrefab, position, rotation, null);
            player.name = $"{playerPrefab.name} [connId={connection.connectionId}]";

            NetworkServer.AddPlayerForConnection(connection, player);
            Debug.Log($"ZenjectNetworkManager.OnServerAddPlayer {player.name}");
        }
        public override void OnStartClient()
        {
            base.OnStartClient();

            RegisterZenjectPrefab(playerPrefab);

            foreach (GameObject prefab in spawnPrefabs)
            {
                RegisterZenjectPrefab(prefab);
            }
        }
        private void RegisterZenjectPrefab(GameObject prefab)
        {
            if (prefab == null)
            {
                return;
            }

            // NetworkManager уже зарегистрировал prefab стандартным способом.
            NetworkClient.UnregisterPrefab(prefab);

            NetworkClient.RegisterPrefab(
                prefab,
                message => instantiator.InstantiatePrefab(
                    prefab,
                    message.position,
                    message.rotation,
                    null),
                spawnedObject => Object.Destroy(spawnedObject));
            
            Debug.Log($"ZenjectNetworkManager.RegisterZenjectPrefab {prefab.name}");
        }

    }
}
