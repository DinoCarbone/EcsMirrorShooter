using System;
using UnityEngine;

namespace ECS.Gameplay.Jump.Components
{
    [Serializable]
    public struct GroundCheckComponent
    {
        public Transform GroundCheckPoint;
        public bool IsGrounded;
    }
}