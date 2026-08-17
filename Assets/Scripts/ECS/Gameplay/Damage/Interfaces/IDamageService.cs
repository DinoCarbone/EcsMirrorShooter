using UnityEngine;

namespace ECS.Gameplay.Damage.Interfaces
{
    public interface IDamageService
    {
        void ApplyDamage(GameObject target, float damage);
    }
}
