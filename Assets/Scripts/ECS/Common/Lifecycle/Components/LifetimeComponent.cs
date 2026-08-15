using System;
using UnityEngine;

namespace ECS.Common.Lifecycle.Components
{
    [Serializable]
    public struct LifetimeComponent
    {
        [Min(0f)] public float RemainingSeconds;
    }
}
