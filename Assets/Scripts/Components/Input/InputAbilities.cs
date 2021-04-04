using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public struct InputAbility : IComponentData
    {
        public bool AbilityOne;

        public bool AbilityTwo;

        public bool LeftHand { get; set; }

        public bool RightHand { get; set; }

        public float2 Looking { get; set; }
    }
}
