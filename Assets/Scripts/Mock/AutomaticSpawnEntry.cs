using System;
using UnityEngine;

namespace Mock
{
    [Serializable]
    public class AutomaticSpawnEntry
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private Quaternion rotation;

        public GameObject Prefab => prefab;
        public Vector3 Position => spawnPoint != null ? spawnPoint : Vector3.zero;
        public Quaternion Rotation => spawnPoint != null ? rotation : Quaternion.identity;
    }
}
