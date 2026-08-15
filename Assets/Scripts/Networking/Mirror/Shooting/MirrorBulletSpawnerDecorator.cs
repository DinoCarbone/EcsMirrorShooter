using System;
using ECS.Gameplay.Shooting.Interfaces;
using global::Mirror;
using Networking.Mirror.Extensions;
using Networking.Mirror.Integration;
using UnityEngine;

namespace Networking.Mirror.Shooting
{
    public class MirrorBulletSpawnerDecorator : IBulletSpawner, IMirrorServerHandler
    {
        private struct SpawnBulletMessage : NetworkMessage
        {
            public uint AssetId;
            public Vector3 Position;
            public Quaternion Rotation;
        }

        private readonly IBulletSpawner bulletSpawner;

        public MirrorBulletSpawnerDecorator(IBulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner ?? throw new ArgumentNullException(nameof(bulletSpawner));
        }

        public GameObject Spawn(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            if (bulletPrefab == null)
            {
                throw new ArgumentNullException(nameof(bulletPrefab));
            }

            if (!bulletPrefab.TryGetComponent(out NetworkIdentity identity))
            {
                throw new InvalidOperationException(
                    $"Bullet prefab '{bulletPrefab.name}' has no {nameof(NetworkIdentity)} component.");
            }

            if (!NetworkClient.ready)
            {
                Debug.LogWarning("Cannot spawn a network bullet while the client is not ready.");
                return null;
            }

            NetworkClient.Send(new SpawnBulletMessage
            {
                AssetId = identity.assetId,
                Position = position,
                Rotation = rotation
            });

            return null;
        }

        public void RegisterHandler()
        {
            NetworkServer.RegisterHandler<SpawnBulletMessage>(HandleSpawn);
        }

        public void UnregisterHandler()
        {
            NetworkServer.UnregisterHandler<SpawnBulletMessage>();
        }

        private void HandleSpawn(
            NetworkConnectionToClient connection,
            SpawnBulletMessage message)
        {
            if (!connection.isReady)
            {
                return;
            }

            GameObject prefab = NetworkManager.singleton.GetPrefab(message.AssetId);
            if (prefab == null)
            {
                Debug.LogWarning($"Cannot spawn bullet: prefab with assetId={message.AssetId} is not registered.");
                return;
            }

            GameObject bullet = bulletSpawner.Spawn(
                prefab,
                message.Position,
                message.Rotation);

            NetworkServer.Spawn(bullet);
        }
    }
}
