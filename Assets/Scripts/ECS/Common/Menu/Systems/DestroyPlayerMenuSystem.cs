using ECS.Common.Menu.Interfaces;
using ECS.Common.Tags;
using Leopotam.Ecs;

namespace ECS.Common.Menu.Systems
{
    public class DestroyPlayerMenuSystem : IEcsRunSystem
    {
        private readonly IPlayerMenuService playerMenuService;

        private EcsFilter<PlayerTag> filter = null;

        public DestroyPlayerMenuSystem(IPlayerMenuService playerMenuService)
        {
            this.playerMenuService = playerMenuService;
        }

        public void Run()
        {
            if (playerMenuService.IsOpen && filter.IsEmpty())
            {
                playerMenuService.Close();
            }
        }
    }
}
