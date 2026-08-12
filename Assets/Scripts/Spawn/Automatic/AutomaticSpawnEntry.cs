using System;
using UnityEngine;

namespace Spawn.Automatic
{
    [Serializable]
    public class AutomaticSpawnEntry
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject Prefab => prefab;
        public Vector3 Position => spawnPoint != null ? spawnPoint.position : Vector3.zero;
        public Quaternion Rotation => spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
    }
}
