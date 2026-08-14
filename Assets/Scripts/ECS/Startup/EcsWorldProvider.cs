using System;
using Leopotam.Ecs;

namespace ECS.Startup
{
    public sealed class EcsWorldProvider : IEcsWorldProvider, IDisposable
    {
        private EcsWorld world = new();

        public EcsWorld World => world;

        public void Dispose()
        {
            if (world == null)
            {
                return;
            }

            if (world.IsAlive())
            {
                world.Destroy();
            }

            world = null;
        }
    }
}
