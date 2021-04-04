using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct VelocityComponent : IComponentData
    {
        public float2 Velocity;

        public float AngularVelocity;
    }
}
