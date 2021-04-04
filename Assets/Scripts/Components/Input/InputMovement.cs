using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct InputMovement : IComponentData
    {
        public float2 Movement { get; set; }

        public float2 Looking { get; set; }

        public bool Jumped { get; set; }
    }
}
