using System;
using ECS.Common.Collision.Components;
using ECS.Gameplay.Damage.Components;
using ECS.Gameplay.Damage.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Damage.Systems
{
    public class ApplyCollisionDamageSystem : IEcsRunSystem
    {
        private readonly IDamageService damageService;
        private EcsFilter<CollisionSignal, DamageComponent> filter = null;

        public ApplyCollisionDamageSystem(IDamageService damageService)
        {
            this.damageService = damageService ?? throw new ArgumentNullException(nameof(damageService));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                GameObject target = filter.Get1(index).OtherEntity;

                if (target == null)
                {
                    continue;
                }

                damageService.ApplyDamage(target, filter.Get2(index).Value);
            }
        }
    }
}
