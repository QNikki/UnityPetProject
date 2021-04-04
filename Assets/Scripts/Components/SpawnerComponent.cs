
using Unity.Entities;

namespace Components
{
    public struct SpawnerComponent : IComponentData
    {
        public float CountX;

        public float CountY;

        public Entity Prfb;
    }
}
