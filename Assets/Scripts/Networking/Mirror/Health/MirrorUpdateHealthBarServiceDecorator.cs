using System;
using ECS.Gameplay.Health.Interfaces;
using ECS.Gameplay.Health.MonoLinks;
using global::Mirror;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Health
{
    public class MirrorUpdateHealthBarServiceDecorator : IUpdateHealthBarService, IMirrorServerHandler
    {
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

            if (!NetworkServer.active)
            {
                return;
            }

            NetworkServer.SendToAll(new SetHealthBarValueMessage
            {
                NetId = identity.netId,
                NormalizedValue = normalizedValue
            });
        }

        public void RegisterHandler()
        {
            NetworkClient.RegisterHandler<SetHealthBarValueMessage>(HandleSetValue);
        }

        public void UnregisterHandler()
        {
            NetworkClient.UnregisterHandler<SetHealthBarValueMessage>();
        }

        private void HandleSetValue(SetHealthBarValueMessage message)
        {
            if (!NetworkClient.spawned.TryGetValue(message.NetId, out NetworkIdentity identity))
            {
                return;
            }

            IHealthBar healthBar = identity.GetComponentInChildren<HealthBarMonoLink>(true);
            if (healthBar == null)
            {
                return;
            }

            updateHealthBarService.SetValue(
                identity.gameObject,
                healthBar,
                message.NormalizedValue);
        }
    }
}
