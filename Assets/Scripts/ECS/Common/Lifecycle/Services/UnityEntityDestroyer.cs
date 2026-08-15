using ECS.Common.Lifecycle.Interfaces;
using UnityEngine;

namespace ECS.Common.Lifecycle.Services
{
    public class UnityEntityDestroyer : IEntityDestroyer
    {
        public bool TryDestroy(GameObject target)
        {
            if (target != null)
            {
                Object.Destroy(target);
            }

            return true;
        }
    }
}
