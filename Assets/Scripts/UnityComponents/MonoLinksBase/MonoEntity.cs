using Leopotam.Ecs;
using UnityEngine;

namespace UnityComponents.MonoLinksBase
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

        public override void Make(ref EcsEntity entity)
        {
            this.entity = entity;

            monoLinks = GetComponents<MonoLinkBase>();
            foreach (MonoLinkBase monoLink in monoLinks)
            {
                if (monoLink is MonoEntity)
                {
                    continue;
                }

                monoLink.Make(ref entity);
            }
        }
    }
}