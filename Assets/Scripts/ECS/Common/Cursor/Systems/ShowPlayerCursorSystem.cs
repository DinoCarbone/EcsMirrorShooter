using ECS.Common.Lifecycle.Components;
using ECS.Common.Tags;
using ECS.Common.Cursor.Components;
using ECS.Common.Cursor.Interfaces;
using Leopotam.Ecs;

namespace ECS.Common.Cursor.Systems
{
    public class ShowPlayerCursorSystem : IEcsRunSystem
    {
        private readonly IPlayerCursorSystem playerCursorSystem;

        private EcsFilter<PlayerTag, CursorHiddenTag, DestroyRequest> filter = null;

        public ShowPlayerCursorSystem(IPlayerCursorSystem playerCursorSystem)
        {
            this.playerCursorSystem = playerCursorSystem;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                playerCursorSystem.Show();
                break;
            }
        }
    }
}
