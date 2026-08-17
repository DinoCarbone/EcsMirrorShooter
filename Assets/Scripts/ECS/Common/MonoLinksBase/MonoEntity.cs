using System;
using ECS.Common.Lifecycle.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.MonoLinksBase
{
    public class MonoEntity : MonoLinkBase
    {
        private EcsEntity entity;

        private MonoLinkBase[] monoLinks;

        public MonoLink<T> Get<T>() where T : struct
        {
            foreach (MonoLinkBase link in monoLinks)
            {
                if (link is MonoLink<T> monoLink)
                {
                    return monoLink;
                }
            }

            return null;
        }

        public void Set<T>(T component) where T : struct
        {
            if (!entity.IsAlive())
            {
                return;
            }

            entity.Get<T>() = component;
        }

        public bool TryGet<T>(out T component) where T : struct
        {
            if (!entity.IsAlive() || !entity.Has<T>())
            {
                component = default;
                return false;
            }

            component = entity.Get<T>();
            return true;
        }

        public override void Make(ref EcsEntity entity)
        {
            if (!entity.IsAlive())
            {
                throw new ArgumentException("Cannot bind MonoEntity to an entity that is not alive.", nameof(entity));
            }

            if (this.entity.IsAlive())
            {
                throw new InvalidOperationException("MonoEntity is already bound to an alive ECS entity.");
            }

            this.entity = entity;
            entity.Get<GameObjectComponent>().Value = gameObject;

            monoLinks = GetComponentsInChildren<MonoLinkBase>();
            foreach (MonoLinkBase monoLink in monoLinks)
            {
                if (monoLink is MonoEntity)
                {
                    continue;
                }

                monoLink.Make(ref entity);
            }
        }

        private void DestroyEntity()
        {
            if (entity.IsAlive())
            {
                entity.Destroy();
            }

            entity = EcsEntity.Null;
        }

        private void OnDestroy()
        {
            DestroyEntity();
        }
    }
}
