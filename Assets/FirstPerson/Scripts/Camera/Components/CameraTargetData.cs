using System;
using Unity.Entities;

namespace FP.Core.Character
{
    [Serializable]
    public struct CameraTargetData : IComponentData
    {
        public Entity Target; 
    }
}