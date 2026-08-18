using ECS.Common.MonoLinksBase;
using ECS.Gameplay.Death.Components;
using ECS.Gameplay.Death.Interfaces;
using ECS.Gameplay.Health.Components;
using UnityEngine;

namespace ECS.Gameplay.Death.Services
{
    public class EcsPlayerDeathService : IPlayerDeathService
    {
        private static readonly Vector3 RespawnPosition = Vector3.zero;

        public void Kill(GameObject player)
        {
            if (player == null ||
                !player.TryGetComponent(out MonoEntity monoEntity) ||
                !monoEntity.TryGet(out HealthComponent health))
            {
                return;
            }

            if (player.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.position = RespawnPosition;
            }
            else
            {
                player.transform.position = RespawnPosition;
            }

            health.Value = health.MaxValue;
            monoEntity.Set(health);
            monoEntity.Set(new HealthChangedSignal());
            monoEntity.Remove<DeathRequestedTag>();
        }
    }
}
