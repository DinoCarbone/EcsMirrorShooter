using System;
using ECS.Common.Lifecycle.Components;
using ECS.Common.Tags;
using ECS.Gameplay.Death.Components;
using ECS.Gameplay.Death.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Death.Systems
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private readonly IPlayerDeathService playerDeathService;

        private EcsFilter<
            PlayerTag,
            DeathRequestedTag,
            GameObjectComponent>
            filter = null;

        public PlayerDeathSystem(IPlayerDeathService playerDeathService)
        {
            this.playerDeathService = playerDeathService ??
                throw new ArgumentNullException(nameof(playerDeathService));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                GameObject player = filter.Get3(index).Value;
                playerDeathService.Kill(player);
            }
        }
    }
}
