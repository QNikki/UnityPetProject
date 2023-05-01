using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Authoring;
using Unity.CharacterController;

namespace FP.Core.Character
{
    [Serializable]
    public struct CharacterData : IComponentData
    {
        public float RotationSharpness;

        public float GroundMaxSpeed;

        public float GroundedMovementSharpness;

        public float AirAcceleration;

        public float AirMaxSpeed;

        public float AirDrag;

        public float3 Gravity;

        public bool PreventAirAccelerationAgainstUngroundedHits;

        public float JumpSpeed;
        
        public int MaxJumpsInAir;

        public BasicStepAndSlopeHandlingParameters StepAndSlopeHandling;
        
        [NonSerialized]
        public int CurrentJumpsInAir;
        
        public CustomPhysicsBodyTags IgnoreCollisionsTag;
        
        public CustomPhysicsBodyTags IgnoreGroundingTag;
        
        public CustomPhysicsBodyTags ZeroMassAgainstCharacterTag;
        
        public CustomPhysicsBodyTags InfiniteMassAgainstCharacterTag;
        
        public CustomPhysicsBodyTags IgnoreStepHandlingTag;
        
        public static CharacterData GetDefault => new CharacterData
        {
            RotationSharpness = 25f,
            GroundMaxSpeed = 10f,
            GroundedMovementSharpness = 15f,
            AirAcceleration = 50f,
            AirMaxSpeed = 10f,
            AirDrag = 0f,
            JumpSpeed = 10f,
            Gravity = math.up() * -30f,
            PreventAirAccelerationAgainstUngroundedHits = true,
            MaxJumpsInAir = 0,
            StepAndSlopeHandling = BasicStepAndSlopeHandlingParameters.GetDefault(),
        };
    }
}