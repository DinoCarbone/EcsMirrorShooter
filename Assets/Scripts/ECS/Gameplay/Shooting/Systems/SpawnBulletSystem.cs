using System;
using ECS.Gameplay.Shooting.Components;
using ECS.Gameplay.Shooting.Interfaces;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Gameplay.Shooting.Systems
{
    public class SpawnBulletSystem : IEcsRunSystem
    {
        private readonly IBulletSpawner bulletSpawner;
        private EcsFilter<SpawnBulletSignal, BulletSpawnPointComponent, BulletPrefabComponent> filter = null;

        public SpawnBulletSystem(IBulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner ?? throw new ArgumentNullException(nameof(bulletSpawner));
        }

        public void Run()
        {
            foreach (int index in filter)
            {
                Transform spawnPoint = filter.Get2(index).Value;
                GameObject bulletPrefab = filter.Get3(index).Value;

                if (spawnPoint == null || bulletPrefab == null)
                {
                    continue;
                }

                bulletSpawner.Spawn(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
