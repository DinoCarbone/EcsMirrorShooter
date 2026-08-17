using UnityEngine;

namespace ECS.Gameplay.Health.Interfaces
{
    public interface IUpdateHealthBarService
    {
        void SetValue(
            GameObject target,
            IHealthBar healthBar,
            float normalizedValue);
    }
}
