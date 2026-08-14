using ECS.Gameplay.Shooting.Interfaces;
using global::Mirror;
using Networking.Mirror.Extensions;
using UnityEngine;

namespace Networking.Mirror.Shooting
{
    public class MirrorBulletSpawnerDecorator : NetworkBehaviour, IBulletSpawner
    {
        private IBulletSpawner bulletSpawner;

        public void Construct(IBulletSpawner bulletSpawner)
        {
            this.bulletSpawner = bulletSpawner;
        }

        public GameObject Spawn(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            var identity = bulletPrefab.GetComponent<NetworkIdentity>();

            CmdSpawn(
                identity.assetId,
                position,
                rotation);
            return null;
        }

        [Command(requiresAuthority = false)]
        private void CmdSpawn(
            uint assetId,
            Vector3 position,
            Quaternion rotation)
        {
            GameObject prefab = NetworkManager.singleton.GetPrefab(assetId);

            GameObject bullet = bulletSpawner.Spawn(
                prefab,
                position,
                rotation);

            NetworkServer.Spawn(bullet);
        }
    }
}
