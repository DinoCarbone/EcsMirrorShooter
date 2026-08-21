using ECS.Common.Collision.Components;
using ECS.Common.MonoLinksBase;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Collision.MonoLinks
{
    public class CollisionMonoLink : MonoLink<CollisionSignal>
    {
        private EcsEntity entity;

        public override void Make(ref EcsEntity entity)
        {
            this.entity = entity;
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (!entity.IsAlive())
            {
                return;
            }

            entity.Get<CollisionSignal>().OtherEntity = collision.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!entity.IsAlive())
            {
                return;
            }

            entity.Get<CollisionSignal>().OtherEntity = other.gameObject;
        }
    }
}
