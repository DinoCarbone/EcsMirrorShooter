using UnityEngine;

namespace ECS.Gameplay.Death.Interfaces
{
    public interface IPlayerDeathService
    {
        void Kill(GameObject player);
    }
}
