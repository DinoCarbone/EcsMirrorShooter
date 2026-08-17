using ECS.Gameplay.Health.Interfaces;
using UnityEngine;

namespace ECS.Gameplay.Health.Services
{
    public class UnityUpdateHealthBarService : IUpdateHealthBarService
    {
        public void SetValue(
            GameObject target,
            IHealthBar healthBar,
            float normalizedValue)
        {
            healthBar?.SetValue(normalizedValue);
        }
    }
}
