using System;
using ECS.Common.MonoLinksBase;
using ECS.Gameplay.Health.Components;
using ECS.Gameplay.Health.Interfaces;
using global::Mirror;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Health
{
    public class MirrorUpdateHealthBarServiceDecorator :
        IUpdateHealthBarService,
        IMirrorServerHandler,
        IMirrorClientHandler
    {
        private struct SetHealthBarValueRequestMessage : NetworkMessage
        {
            public uint NetId;
            public float NormalizedValue;
        }

        private struct SetHealthBarValueMessage : NetworkMessage
        {
            public uint NetId;
            public float NormalizedValue;
        }

        private readonly IUpdateHealthBarService updateHealthBarService;

        public MirrorUpdateHealthBarServiceDecorator(IUpdateHealthBarService updateHealthBarService)
        {
            this.updateHealthBarService = updateHealthBarService ??
                throw new ArgumentNullException(nameof(updateHealthBarService));
        }

        public void SetValue(
            GameObject target,
            IHealthBar healthBar,
            float normalizedValue)
        {
            if (target == null ||
                !target.TryGetComponent(out NetworkIdentity identity) ||
                identity.netId == 0)
            {
                updateHealthBarService.SetValue(target, healthBar, normalizedValue);
                return;
            }

            if (NetworkServer.active && identity.isServer)
            {
                SendValueToClients(identity.netId, normalizedValue);
                return;
            }

            if (!NetworkClient.ready)
            {
                return;
            }

            NetworkClient.Send(new SetHealthBarValueRequestMessage
            {
                NetId = identity.netId,
                NormalizedValue = normalizedValue
            });
        }

        public void RegisterServerHandler()
        {
            NetworkServer.RegisterHandler<SetHealthBarValueRequestMessage>(HandleSetValueRequest);
        }

        public void UnregisterServerHandler()
        {
            NetworkServer.UnregisterHandler<SetHealthBarValueRequestMessage>();
        }

        public void RegisterClientHandler()
        {
            NetworkClient.RegisterHandler<SetHealthBarValueMessage>(HandleSetValue);
        }

        public void UnregisterClientHandler()
        {
            NetworkClient.UnregisterHandler<SetHealthBarValueMessage>();
        }

        private void HandleSetValueRequest(
            NetworkConnectionToClient connection,
            SetHealthBarValueRequestMessage message)
        {
            if (!connection.isReady ||
                !NetworkServer.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }

            if (identity.connectionToClient != connection)
            {
                Debug.LogWarning(
                    $"Connection {connection.connectionId} cannot update health bar " +
                    $"for network object with netId={message.NetId} because it has no authority.");
                return;
            }

            SendValueToClients(message.NetId, message.NormalizedValue);
        }

        private void SendValueToClients(uint netId, float normalizedValue)
        {
            NetworkServer.SendToAll(
                new SetHealthBarValueMessage
                {
                    NetId = netId,
                    NormalizedValue = normalizedValue
                },
                Channels.Reliable,
                true);
        }

        private void HandleSetValue(SetHealthBarValueMessage message)
        {
            if (!NetworkClient.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }

            if (!identity.isOwned ||
                !identity.TryGetComponent(out MonoEntity monoEntity) ||
                !monoEntity.TryGet(out HealthBarComponent healthBar))
            {
                return;
            }

            updateHealthBarService.SetValue(
                identity.gameObject,
                healthBar.Value,
                message.NormalizedValue);
        }
    }
}
