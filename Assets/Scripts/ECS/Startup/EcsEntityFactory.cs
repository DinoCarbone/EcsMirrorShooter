using System;
using Leopotam.Ecs;
using ECS.Common.MonoLinksBase;
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
            GameObject instance = CreatePrefab(prefab, position, rotation);
            return MakeEntityIfPresent(instance);
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
            Type requestedType = typeof(T);
            if (requestedType != typeof(GameObject) &&
                !typeof(Component).IsAssignableFrom(requestedType))
            {
                throw new InvalidOperationException(
                    $"Type {requestedType.Name} must be {nameof(GameObject)} or a {nameof(Component)}.");
            }

            GameObject instance = CreatePrefab(prefab, position, rotation);
            MakeEntityIfPresent(instance);

            if (requestedType == typeof(GameObject))
            {
                return (T)(UnityEngine.Object)instance;
            }

            T component = instance.GetComponentInChildren(requestedType, true) as T;
            if (component == null)
            {
                throw new InvalidOperationException(
                    $"Prefab '{prefab.name}' does not contain component {requestedType.Name}.");
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

        private GameObject CreatePrefab(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            return instantiator.InstantiatePrefab(
                prefab,
                position,
                rotation,
                null);
        }

        private EcsEntity MakeEntityIfPresent(GameObject instance)
        {
            MonoEntity monoEntity = instance.GetComponentInChildren<MonoEntity>(true);
            if (monoEntity == null)
            {
                return EcsEntity.Null;
            }

            return MakeEntity(monoEntity);
        }

        private EcsEntity MakeEntity(MonoEntity monoEntity)
        {
            EcsEntity entity = worldProvider.World.NewEntity();
            monoEntity.Make(ref entity);
            return entity;
        }
    }
}
