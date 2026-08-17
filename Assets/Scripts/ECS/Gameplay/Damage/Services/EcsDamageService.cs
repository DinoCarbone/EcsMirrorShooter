using ECS.Common.MonoLinksBase;
using ECS.Gameplay.Damage.Components;
using ECS.Gameplay.Damage.Interfaces;
using UnityEngine;

namespace ECS.Gameplay.Damage.Services
{
    public class EcsDamageService : IDamageService
    {
        public void ApplyDamage(GameObject target, float damage)
        {
            if (target != null && target.TryGetComponent(out MonoEntity monoEntity))
            {
                DamageSignal damageSignal = new DamageSignal
                {
                    Value = damage
                };

                if (monoEntity.TryGet(out DamageSignal currentDamageSignal))
                {
                    damageSignal.Value += currentDamageSignal.Value;
                }
                monoEntity.Set(damageSignal);
            }
        }
    }
}
