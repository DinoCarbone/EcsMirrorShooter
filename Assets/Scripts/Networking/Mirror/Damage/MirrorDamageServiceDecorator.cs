using System;
using ECS.Gameplay.Damage.Interfaces;
using global::Mirror;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Damage
{
    public class MirrorDamageServiceDecorator : IDamageService, IMirrorServerHandler
    {
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

            if (!NetworkClient.ready)
            {
                return;
            }

            NetworkClient.Send(new ApplyDamageMessage
            {
                NetId = identity.netId,
                Damage = damage
            });
        }

        public void RegisterHandler()
        {
            NetworkServer.RegisterHandler<ApplyDamageMessage>(HandleApplyDamage);
        }

        public void UnregisterHandler()
        {
            NetworkServer.UnregisterHandler<ApplyDamageMessage>();
        }

        private void HandleApplyDamage(
            NetworkConnectionToClient connection,
            ApplyDamageMessage message)
        {
            if (!connection.isReady ||
                !NetworkServer.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }
            Debug.Log(identity.gameObject.name);
            damageService.ApplyDamage(identity.gameObject, message.Damage);
        }
    }
}
