using System;
using ECS.Common.Camera.Interfaces;
using Unity.Cinemachine;
using UnityEngine;

namespace ECS.Common.Camera.Services
{
    public class CinemachinePlayerCameraService : IPlayerCameraService
    {
        private readonly CinemachineCamera cinemachineCamera;

        public CinemachinePlayerCameraService(CinemachineCamera cinemachineCamera)
        {
            this.cinemachineCamera = cinemachineCamera ??
                throw new ArgumentNullException(nameof(cinemachineCamera));
        }

        public void SetTarget(Transform target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            cinemachineCamera.Follow = target;
        }

        public void ClearTarget()
        {
            cinemachineCamera.Follow = null;
        }
    }
}
