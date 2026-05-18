using System;
using UnityEngine;

namespace ECS.Gameplay.Movement.Components
{
    [Serializable]
    public struct GroundCheckComponent
    {
        public Transform GroundCheckPoint;
        public bool IsGrounded;
    }
}