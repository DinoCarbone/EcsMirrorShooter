using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Common.MonoLinksBase
{
    public abstract class MonoLinkBase : MonoBehaviour
    {
        public abstract void Make(ref EcsEntity entity);
    }
}
