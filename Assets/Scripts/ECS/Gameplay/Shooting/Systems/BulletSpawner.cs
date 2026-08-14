using System;
using ECS.Gameplay.Shooting.Interfaces;
using ECS.Startup;
using UnityEngine;

namespace ECS.Gameplay.Shooting.Systems
{
    public class BulletSpawner : IBulletSpawner
    {
        private readonly IEcsEntityFactory ecsEntityFactory;

        public BulletSpawner(IEcsEntityFactory ecsEntityFactory)
        {
            this.ecsEntityFactory = ecsEntityFactory;
        }
        public GameObject Spawn(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            if (bulletPrefab == null)
            {
                throw new ArgumentNullException(nameof(bulletPrefab));
            }
            
            return ecsEntityFactory.Create<GameObject>(
                bulletPrefab,
                position,
                rotation);
        }
    }
}
