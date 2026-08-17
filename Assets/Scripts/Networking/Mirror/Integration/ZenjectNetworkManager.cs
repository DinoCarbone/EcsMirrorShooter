using global::Mirror;
using UnityEngine;
using Zenject;

namespace Networking.Mirror.Integration
{
    public class ZenjectNetworkManager : NetworkManager
    {
        private IInstantiator instantiator;
        private IMirrorServerHandlersProxy serverHandlersProxy;
        private IMirrorClientsHandlersProxy clientsHandlersProxy;

        [Inject]
        public void Construct(
            IInstantiator prefabInstantiator,
            IMirrorServerHandlersProxy serverHandlersProxy,
            IMirrorClientsHandlersProxy clientsHandlersProxy)
        {
            instantiator = prefabInstantiator;
            this.serverHandlersProxy = serverHandlersProxy;
            this.clientsHandlersProxy = clientsHandlersProxy;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            serverHandlersProxy.RegisterServerHandlers();
        }

        public override void OnStopServer()
        {
            serverHandlersProxy.UnregisterServerHandlers();
            base.OnStopServer();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            clientsHandlersProxy.RegisterClientHandlers();

            RegisterZenjectPrefab(playerPrefab);

            foreach (GameObject prefab in spawnPrefabs)
            {
                RegisterZenjectPrefab(prefab);
            }
        }

        public override void OnStopClient()
        {
            clientsHandlersProxy.UnregisterClientHandlers();
            base.OnStopClient();
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
