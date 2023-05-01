using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace FP.Core.Character
{
    [Serializable]
    public struct InputData : IComponentData
    {
        public float2 Move;
        
        public float2 CameraLook;
        
        public float CameraZoom;
        
        public FixedInputEvent JumpPressed;
    }
}