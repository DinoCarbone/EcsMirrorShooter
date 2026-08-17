using System;
using UnityEngine;

namespace ECS.Gameplay.Health.Components
{
    [Serializable]
    public struct HealthComponent
    {
        public float MaxValue;
        [HideInInspector]
        public float Value;
    }
}
