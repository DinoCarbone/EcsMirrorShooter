using System;
using ECS.Gameplay.Damage.Interfaces;
using global::Mirror;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Damage
{
    public class MirrorDamageServiceDecorator :
        IDamageService,
        IMirrorServerHandler,
        IMirrorClientHandler
    {
        private struct DamageRequestMessage : NetworkMessage
        {
            public uint NetId;
            public float Damage;
        }

        private struct ApplyDamageMessage : NetworkMessage
        {
            public uint NetId;
            public float Damage;
        }

        private readonly IDamageService damageService;

        public MirrorDamageServiceDecorator(IDamageService damageService)
        {
            this.damageService = damageService ??
                throw new ArgumentNullException(nameof(damageService));
        }

        public void ApplyDamage(GameObject target, float damage)
        {
            if (target == null ||
                !target.TryGetComponent(out NetworkIdentity identity) ||
                identity.netId == 0)
            {
                damageService.ApplyDamage(target, damage);
                return;
            }

            if (NetworkServer.active && identity.isServer)
            {
                SendDamage(identity, damage);
                return;
            }

            if (!NetworkClient.ready)
            {
                return;
            }

            NetworkClient.Send(new DamageRequestMessage
            {
                NetId = identity.netId,
                Damage = damage
            });
        }

        public void RegisterServerHandler()
        {
            NetworkServer.RegisterHandler<DamageRequestMessage>(HandleDamageRequest);
        }

        public void UnregisterServerHandler()
        {
            NetworkServer.UnregisterHandler<DamageRequestMessage>();
        }

        public void RegisterClientHandler()
        {
            NetworkClient.RegisterHandler<ApplyDamageMessage>(HandleApplyDamage);
        }

        public void UnregisterClientHandler()
        {
            NetworkClient.UnregisterHandler<ApplyDamageMessage>();
        }

        private void HandleDamageRequest(
            NetworkConnectionToClient connection,
            DamageRequestMessage message)
        {
            if (!connection.isReady ||
                !NetworkServer.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }

            SendDamage(identity, message.Damage);
        }

        private void SendDamage(NetworkIdentity identity, float damage)
        {
            NetworkConnectionToClient targetClient = identity.connectionToClient;
            if (targetClient == null || !targetClient.isReady)
            {
                return;
            }
            
            targetClient.Send(new ApplyDamageMessage
            {
                NetId = identity.netId,
                Damage = damage
            });
        }

        private void HandleApplyDamage(ApplyDamageMessage message)
        {
            if (!NetworkClient.spawned.TryGetValue(message.NetId, out NetworkIdentity identity) ||
                !identity.isOwned)
            {
                return;
            }
            Debug.Log(1);
            damageService.ApplyDamage(identity.gameObject, message.Damage);
        }
    }
}
