using System;
using Leopotam.Ecs;
using UnityComponents.MonoLinksBase;
using UnityEngine;
using Zenject;

namespace ECS.Startup
{
    public class EcsEntityFactory : IEcsEntityFactory
    {
        private readonly IEcsWorldProvider worldProvider;
        private readonly IInstantiator instantiator;

        public EcsEntityFactory(
            IEcsWorldProvider worldProvider,
            IInstantiator instantiator)
        {
            this.worldProvider = worldProvider;
            this.instantiator = instantiator;
        }

        public EcsEntity Create(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            MonoEntity monoEntity = CreateMonoEntity(prefab, position, rotation);
            return MakeEntity(monoEntity);
        }

        public EcsEntity Create(GameObject prefab, Transform spawnPoint)
        {
            if (spawnPoint == null)
            {
                throw new ArgumentNullException(nameof(spawnPoint));
            }

            return Create(prefab, spawnPoint.position, spawnPoint.rotation);
        }

        public T Create<T>(GameObject prefab, Vector3 position, Quaternion rotation)
            where T : UnityEngine.Object
        {
            MonoEntity monoEntity = CreateMonoEntity(prefab, position, rotation);
            MakeEntity(monoEntity);

            if (typeof(T) == typeof(GameObject))
            {
                return (T)(UnityEngine.Object)monoEntity.gameObject;
            }

            if (!typeof(Component).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException(
                    $"Type {typeof(T).Name} must be {nameof(GameObject)} or a {nameof(Component)}.");
            }

            T component = monoEntity.GetComponentInChildren(typeof(T), true) as T;
            if (component == null)
            {
                throw new InvalidOperationException(
                    $"Prefab '{prefab.name}' does not contain component {typeof(T).Name}.");
            }

            return component;
        }

        public T Create<T>(GameObject prefab, Transform spawnPoint = null)
            where T : UnityEngine.Object
        {
            Vector3 position = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;

            return Create<T>(prefab, position, rotation);
        }

        private MonoEntity CreateMonoEntity(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            MonoEntity monoEntity = instantiator.InstantiatePrefabForComponent<MonoEntity>(
                prefab,
                position,
                rotation,
                null);

            if (monoEntity == null)
            {
                throw new InvalidOperationException(
                    $"Prefab '{prefab.name}' does not contain {nameof(MonoEntity)}.");
            }

            return monoEntity;
        }

        private EcsEntity MakeEntity(MonoEntity monoEntity)
        {
            EcsEntity entity = worldProvider.World.NewEntity();
            monoEntity.Make(ref entity);
            return entity;
        }
    }
}
