using System;
using ECS.Gameplay.Movement.Components;
using ECS.Gameplay.Shooting.Interfaces;
using ECS.Startup;
using ECS.Common.MonoLinksBase;
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
            GameObject bulletObject = ecsEntityFactory.Create<GameObject>(
                bulletPrefab,
                position,
                rotation);

            if (bulletObject.TryGetComponent(out MonoEntity monoEntity))
            {
                monoEntity.Set(new MoveInputComponent
                {
                    Value = Vector2.up
                });
            }
            else
            {
                Debug.LogWarning("Bullet object has no MonoEntity component");
            }

            return bulletObject;
        }
    }
}
