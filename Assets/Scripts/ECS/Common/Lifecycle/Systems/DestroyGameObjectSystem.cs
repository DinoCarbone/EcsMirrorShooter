using System;
using ECS.Common.Lifecycle.Components;
using ECS.Common.Lifecycle.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Lifecycle.Systems
{
    public class DestroyGameObjectSystem : IEcsRunSystem
    {
        private readonly IEntityDestroyer entityDestroyer;
        private EcsFilter<DestroyRequest, GameObjectComponent> filter = null;

        public DestroyGameObjectSystem(IEntityDestroyer entityDestroyer)
        {
            this.entityDestroyer = entityDestroyer ??
                throw new ArgumentNullException(nameof(entityDestroyer));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                GameObject target = filter.Get2(index).Value;
                entityDestroyer.TryDestroy(target);
            }
        }
    }
}
