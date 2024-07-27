using System;
using Unity.CharacterController;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace DZM.Character
{
    // MONKAS
    [Serializable]
    public struct CharComponentData : IComponentData
    {
        public float BaseFoV;

        public float GroundMaxSpeed;

        public float GroundedMovementSharpness;

        public float AirAcceleration;

        public float AirMaxSpeed;

        public float AirDrag;

        public float JumpSpeed;

        public float3 Gravity;

        public bool PreventAirAccelerationAgainstUngroundedHits;

        public BasicStepAndSlopeHandlingParameters StepAndSlopeHandling;

        public Entity ViewEntity;

        public float MinViewAngle;

        public float MaxViewAngle;

        public float ViewRollAmount;

        public float ViewRollSharpness;

        [HideInInspector] public float CharacterYDegrees;

        [HideInInspector] public float ViewPitchDegrees;

        [HideInInspector] public quaternion ViewLocalRotation;

        [HideInInspector] public float ViewRollDegrees;

        [HideInInspector] public byte HasProcessedDeath;

        public static CharComponentData GetDefault()
        {
            return new CharComponentData
            {
                BaseFoV = 75f,
                GroundMaxSpeed = 10f,
                GroundedMovementSharpness = 15f,
                AirAcceleration = 50f,
                AirMaxSpeed = 10f,
                AirDrag = 0f,
                JumpSpeed = 10f,
                Gravity = math.up() * -30f,
                PreventAirAccelerationAgainstUngroundedHits = true,

                StepAndSlopeHandling = BasicStepAndSlopeHandlingParameters.GetDefault(),

                MinViewAngle = -90f,
                MaxViewAngle = 90f,
            };
        }
    }
}