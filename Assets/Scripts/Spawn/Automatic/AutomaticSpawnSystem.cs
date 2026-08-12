using System.Collections.Generic;
using ECS.Startup;
using Zenject;

namespace Spawn.Automatic
{
    public class AutomaticSpawnSystem : IInitializable
    {
        private readonly IReadOnlyList<AutomaticSpawnEntry> entries;
        private readonly IEcsEntityFactory ecsEntityFactory;

        public AutomaticSpawnSystem(
            IReadOnlyList<AutomaticSpawnEntry> entries,
            IEcsEntityFactory ecsEntityFactory)
        {
            this.entries = entries;
            this.ecsEntityFactory = ecsEntityFactory;
        }

        public void Initialize()
        {
            foreach (AutomaticSpawnEntry entry in entries)
            {
                ecsEntityFactory.Create(entry.Prefab, entry.Position, entry.Rotation);
            }
        }
    }
}
