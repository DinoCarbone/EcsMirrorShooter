using ECS.Common.Tags;
using ECS.Common.Cursor.Components;
using ECS.Common.Cursor.Interfaces;
using Leopotam.Ecs;

namespace ECS.Common.Cursor.Systems
{
    public class HidePlayerCursorSystem : IEcsRunSystem
    {
        private readonly IPlayerCursorSystem playerCursorSystem;

        private EcsFilter<PlayerTag>.Exclude<CursorHiddenTag> filter = null;

        public HidePlayerCursorSystem(IPlayerCursorSystem playerCursorSystem)
        {
            this.playerCursorSystem = playerCursorSystem;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                playerCursorSystem.Hide();
                filter.GetEntity(index).Get<CursorHiddenTag>();
                break;
            }
        }
    }
}
