using UnityEngine;

namespace ECS.Gameplay.Shooting.Interfaces
{
    public interface IBulletSpawner
    {
        GameObject Spawn(GameObject bulletPrefab, Vector3 position, Quaternion rotation);
    }
}
