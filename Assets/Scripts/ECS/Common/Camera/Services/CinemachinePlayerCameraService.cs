using System;
using ECS.Common.Camera.Interfaces;
using Unity.Cinemachine;
using UnityEngine;

namespace ECS.Common.Camera.Services
{
    public class CinemachinePlayerCameraService : IPlayerCameraSystem
    {
        private readonly CinemachineCamera cinemachineCamera;
        private readonly CinemachinePanTilt panTilt;
        private readonly CinemachineInputAxisController inputAxisController;

        public CinemachinePlayerCameraService(CinemachineCamera cinemachineCamera)
        {
            this.cinemachineCamera = cinemachineCamera ??
                throw new ArgumentNullException(nameof(cinemachineCamera));

            panTilt = GetRequiredComponent<CinemachinePanTilt>();
            inputAxisController = GetRequiredComponent<CinemachineInputAxisController>();

            if (cinemachineCamera.Follow == null)
            {
                DisableRotation();
            }
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

        public void EnableRotation()
        {
            panTilt.enabled = true;
            inputAxisController.enabled = true;
        }

        public void DisableRotation()
        {
            inputAxisController.enabled = false;
            panTilt.enabled = false;
        }

        private T GetRequiredComponent<T>() where T : Component
        {
            if (cinemachineCamera.TryGetComponent(out T component))
            {
                return component;
            }

            throw new InvalidOperationException(
                $"{cinemachineCamera.name} requires {typeof(T).Name} component.");
        }
    }
}
