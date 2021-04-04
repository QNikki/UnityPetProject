using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    internal struct PositionComponent : IComponentData
    {
        public float3 Position { get; set; }

        public float4 Rotation { get; set; }
    }
}
