using ECS.Common.Cursor.Interfaces;
using ECS.Common.Tags;
using ECS.Gameplay.Jump.Components;
using ECS.Gameplay.Movement.Components;
using ECS.Gameplay.Rotation.Components;
using ECS.Gameplay.Shooting.Components;
using Leopotam.Ecs;

namespace ECS.Common.Menu.Systems
{
    public class ResetDisabledPlayerInputSystem : IEcsRunSystem
    {
        private readonly IPlayerCursorSystem playerCursorSystem;

        private EcsFilter<PlayerTag> playerFilter = null;
        private EcsFilter<PlayerTag, PlayerControlDisabledTag> disabledPlayerFilter = null;
        private EcsFilter<
            PlayerTag,
            PlayerControlDisabledTag,
            MoveInputComponent,
            JumpInputComponent,
            ShootInputComponent> disabledPlayerInputFilter = null;
        private EcsFilter<
            PlayerTag,
            PlayerControlDisabledTag,
            RotationInputComponent> disabledPlayerRotationInputFilter = null;

        private bool wasControlDisabled;

        public ResetDisabledPlayerInputSystem(IPlayerCursorSystem playerCursorSystem)
        {
            this.playerCursorSystem = playerCursorSystem;
        }

        public void Run()
        {
            bool isControlDisabled = !disabledPlayerFilter.IsEmpty();

            if (isControlDisabled)
            {
                ResetInput();
            }

            if (isControlDisabled == wasControlDisabled)
            {
                return;
            }

            wasControlDisabled = isControlDisabled;

            if (isControlDisabled)
            {
                playerCursorSystem.Show();
            }
            else if (!playerFilter.IsEmpty())
            {
                playerCursorSystem.Hide();
            }
        }

        private void ResetInput()
        {
            foreach (int index in disabledPlayerRotationInputFilter)
            {
                ref RotationInputComponent rotationInput = ref
                    disabledPlayerRotationInputFilter.Get3(index);
                rotationInput = default;
            }

            foreach (int index in disabledPlayerInputFilter)
            {
                disabledPlayerInputFilter.Get3(index).Value = default;
                disabledPlayerInputFilter.Get4(index).Value = false;
                disabledPlayerInputFilter.Get5(index).Value = false;

                ref EcsEntity entity = ref disabledPlayerInputFilter.GetEntity(index);
                if (entity.Has<SpawnBulletSignal>())
                {
                    entity.Del<SpawnBulletSignal>();
                }
            }
        }
    }
}
