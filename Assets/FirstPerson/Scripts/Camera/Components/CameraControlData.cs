using System;
using Unity.Entities;
using Unity.Mathematics;

namespace FP.Core.Character
{
    [Serializable]
    public struct CameraControlData: IComponentData
    {
        public Entity FollowedEntity;
        
        public float2 Look;
        
        public float Zoom;
    }
}