using Unity.Entities;
using Unity.Mathematics;

namespace DZM.Character
{
    public struct CharControlComponentData : IComponentData
    {
        public float3 MoveVector;

        public float2 LookYawPitchDegrees;

        public bool Jump;
    }
}