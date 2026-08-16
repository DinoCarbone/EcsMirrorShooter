using ECS.Common.Lifecycle.Components;
using ECS.Common.Menu.Interfaces;
using ECS.Common.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.Menu.Systems
{
    public class TogglePlayerMenuSystem : IEcsRunSystem
    {
        private readonly IPlayerMenuService playerMenuService;

        private readonly EcsFilter<PlayerTag> filter = null;

        public TogglePlayerMenuSystem(IPlayerMenuService playerMenuService)
        {
            this.playerMenuService = playerMenuService;
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    break;
                }

                ref EcsEntity playerEntity = ref filter.GetEntity(index);

                if (playerMenuService.IsOpen)
                {
                    ResumeGameplay(ref playerEntity);
                }
                else
                {
                    OpenMenu(ref playerEntity);
                }

                break;
            }
        }

        private void OpenMenu(ref EcsEntity playerEntity)
        {
            playerMenuService.Open();
            playerEntity.Get<PlayerControlDisabledTag>();
        }

        private void ResumeGameplay(ref EcsEntity playerEntity)
        {
            playerMenuService.Close();

            if (playerEntity.Has<PlayerControlDisabledTag>())
            {
                playerEntity.Del<PlayerControlDisabledTag>();
            }
        }
    }
}
