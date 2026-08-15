using UnityEngine;

namespace ECS.Common.Lifecycle.Interfaces
{
    public interface IEntityDestroyer
    {
        bool TryDestroy(GameObject target);
    }
}
