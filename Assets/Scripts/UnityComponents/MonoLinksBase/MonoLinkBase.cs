using Leopotam.Ecs;
using UnityEngine;

namespace UnityComponents.MonoLinksBase
{
    public abstract class MonoLinkBase : MonoBehaviour
    {
        public abstract void Make(ref EcsEntity entity);
    }
}