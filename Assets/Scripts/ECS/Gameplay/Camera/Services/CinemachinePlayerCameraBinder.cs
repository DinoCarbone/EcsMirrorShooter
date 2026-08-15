using System;
using ECS.Gameplay.Camera.Interfaces;
using Unity.Cinemachine;
using UnityEngine;

namespace ECS.Gameplay.Camera.Services
{
    public class CinemachinePlayerCameraBinder : IPlayerCameraBinder
    {
        private readonly CinemachineCamera cinemachineCamera;

        public CinemachinePlayerCameraBinder(CinemachineCamera cinemachineCamera)
        {
            this.cinemachineCamera = cinemachineCamera;
        }

        public void SetTarget(Transform target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            cinemachineCamera.Follow = target;
        }
    }
}
