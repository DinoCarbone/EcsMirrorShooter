using ECS.Gameplay.Shooting.Components;
using ECS.Common.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Shooting.Systems
{
    public class PlayerShootInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, ShootInputComponent> filter = null;

        public void Run()
        {
            foreach (int index in filter)
            {
                bool shootPressed = Input.GetMouseButtonDown(0);
                ref ShootInputComponent shootInput = ref filter.Get2(index);
                shootInput.Value = shootPressed;

                if (shootPressed)
                {
                    filter.GetEntity(index).Get<SpawnBulletSignal>();
                }
            }
        }
    }
}
