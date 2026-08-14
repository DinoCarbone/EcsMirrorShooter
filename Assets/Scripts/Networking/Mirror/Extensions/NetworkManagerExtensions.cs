using global::Mirror;
using UnityEngine;

namespace Networking.Mirror.Extensions
{
    public static class NetworkManagerExtensions
    {
        public static GameObject GetPrefab(this NetworkManager networkManager, uint assetId)
        {
            foreach (var prefab in networkManager.spawnPrefabs)
            {
                var identity = prefab.GetComponent<NetworkIdentity>();

                if (identity != null && identity.assetId == assetId)
                    return prefab;
            }

            return null;
        }
    }
}
