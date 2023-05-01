using System;
using Unity.Entities;
using Unity.Mathematics;

namespace FP.Core.Character
{
    [Serializable]
    public struct CharacterControlData : IComponentData
    {
        public float3 MoveVector;
        
        public bool Jump;
    }
}