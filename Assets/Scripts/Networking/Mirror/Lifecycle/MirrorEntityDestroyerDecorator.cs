using System;
using ECS.Common.Lifecycle.Interfaces;
using global::Mirror;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Lifecycle
{
    public class MirrorEntityDestroyerDecorator : IEntityDestroyer, IMirrorServerHandler
    {
        private struct DestroyEntityMessage : NetworkMessage
        {
            public uint NetId;
        }

        private readonly IEntityDestroyer entityDestroyer;

        public MirrorEntityDestroyerDecorator(IEntityDestroyer entityDestroyer)
        {
            this.entityDestroyer = entityDestroyer ??
                throw new ArgumentNullException(nameof(entityDestroyer));
        }

        public bool TryDestroy(GameObject target)
        {
            if (target == null ||
                !target.TryGetComponent(out NetworkIdentity identity) ||
                identity.netId == 0)
            {
                return entityDestroyer.TryDestroy(target);
            }

            if (NetworkServer.active && identity.isServer)
            {
                NetworkServer.Destroy(target);
                return true;
            }

            if (!NetworkClient.ready)
            {
                return false;
            }

            NetworkClient.Send(new DestroyEntityMessage
            {
                NetId = identity.netId
            });

            return true;
        }

        public void RegisterHandler()
        {
            NetworkServer.RegisterHandler<DestroyEntityMessage>(HandleDestroy);
        }

        public void UnregisterHandler()
        {
            NetworkServer.UnregisterHandler<DestroyEntityMessage>();
        }

        private void HandleDestroy(
            NetworkConnectionToClient connection,
            DestroyEntityMessage message)
        {
            if (!connection.isReady ||
                !NetworkServer.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }

            if (identity.connectionToClient != connection)
            {
                Debug.LogWarning(
                    $"Connection {connection.connectionId} cannot destroy " +
                    $"network object with netId={message.NetId} because it has no authority.");
                return;
            }

            NetworkServer.Destroy(identity.gameObject);
        }
    }
}
