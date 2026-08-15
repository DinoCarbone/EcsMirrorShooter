using System;
using ECS.Common.Lifecycle.Interfaces;
using global::Mirror;
using UnityEngine;

namespace Networking.Mirror.Lifecycle
{
    public class MirrorEntityDestroyerDecorator : IEntityDestroyer
    {
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

            if (!NetworkServer.active || !identity.isServer)
            {
                return false;
            }

            NetworkServer.Destroy(target);
            return true;
        }
    }
}
