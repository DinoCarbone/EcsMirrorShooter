using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Startup
{
    public interface IEcsWorldProvider
    {
        EcsWorld World { get; }
    }

    public interface IEcsEntityFactory
    {
        EcsEntity Create(GameObject prefab, Vector3 position, Quaternion rotation);
        EcsEntity Create(GameObject prefab, Transform spawnPoint = null);
        T Create<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : UnityEngine.Object;
        T Create<T>(GameObject prefab, Transform spawnPoint = null) where T : UnityEngine.Object;
    }
}
