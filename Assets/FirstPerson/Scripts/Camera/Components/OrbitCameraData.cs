using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace FP.Core.Character
{
    [Serializable]
    public struct OrbitCameraData : IComponentData
    {
        public float RotationSpeed;

        public float MaxVAngle;

        public float MinVAngle;

        public bool RotateWithCharacterParent;

        public float TargetDistance;

        public float MinDistance;

        public float MaxDistance;

        public float DistanceMovementSpeed;

        public float DistanceMovementSharpness;

        public float ObstructionRadius;

        public float ObstructionInnerSmoothingSharpness;

        public float ObstructionOuterSmoothingSharpness;

        public bool PreventFixedUpdateJitter;

        [HideInInspector]
        public float CurrentDistanceFromMovement;

        [HideInInspector]
        public float CurrentDistanceFromObstruction;

        [HideInInspector]
        public float PitchAngle;
        
        [HideInInspector]
        public float3 PlanarForward;

        public static OrbitCameraData Default => new()
        {
            RotationSpeed = 150f,
            MaxVAngle = 89f,
            MinVAngle = -89f,
            TargetDistance = 5f,
            MinDistance = 0f,
            MaxDistance = 10f,
            DistanceMovementSpeed = 50f,
            DistanceMovementSharpness = 20f,
            ObstructionRadius = 0.1f,
            ObstructionInnerSmoothingSharpness = float.MaxValue,
            ObstructionOuterSmoothingSharpness = 5f,
            PreventFixedUpdateJitter = true,
            CurrentDistanceFromObstruction = 0f
        };
    }
}