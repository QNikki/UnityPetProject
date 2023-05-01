using Unity.Physics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.CharacterController;

namespace FP.Core.Character
{
    public readonly partial struct CharacterAspect : IAspect
    {
        private readonly KinematicCharacterAspect _charAspect;

        private readonly RefRW<CharacterData> _refCharData;

        private readonly RefRW<CharacterControlData> _refControlData;

        public void PhysicsUpdate(ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext)
        {
            ref var rwCharData = ref _refCharData.ValueRW;
            ref var rwBody = ref _charAspect.CharacterBody.ValueRW;
            ref var charPosition = ref _charAspect.LocalTransform.ValueRW.Position;

            // First phase of default character update
            _charAspect.Update_Initialize(in this, ref context, ref baseContext, ref rwBody,
                baseContext.Time.DeltaTime);

            _charAspect.Update_ParentMovement(in this, ref context, ref baseContext, ref rwBody,
                ref charPosition, rwBody.WasGroundedBeforeCharacterUpdate);

            _charAspect.Update_Grounding(in this, ref context, ref baseContext, ref rwBody,
                ref charPosition);
            //
            // // Update desired character velocity after grounding was detected, but before doing additional processing that depends on velocity
            HandleVelocityControl(ref context, ref baseContext);
            //
            // Second phase of default character update
            _charAspect.Update_PreventGroundingFromFutureSlopeChange(in this, ref context, ref baseContext,
                ref rwBody, in rwCharData.StepAndSlopeHandling);
            //
            _charAspect.Update_GroundPushing(in this, ref context, ref baseContext, rwCharData.Gravity);
            _charAspect.Update_MovementAndDecollisions(in this, ref context, ref baseContext, ref rwBody,
                ref charPosition);

            _charAspect.Update_MovingPlatformDetection(ref baseContext, ref rwBody);
            _charAspect.Update_ParentMomentum(ref baseContext, ref rwBody);
            _charAspect.Update_ProcessStatefulCharacterHits();
        }

    private void HandleVelocityControl(ref CharacterUpdateContext context, ref KinematicCharacterUpdateContext baseContext)
    {
        float deltaTime = baseContext.Time.DeltaTime;
        ref KinematicCharacterBody characterBody = ref _charAspect.CharacterBody.ValueRW;
        ref CharacterData characterComponent = ref _refCharData.ValueRW;
        ref CharacterControlData characterControl = ref _refControlData.ValueRW;

        // Rotate move input and velocity to take into account parent rotation
        if(characterBody.ParentEntity != Entity.Null)
        {
            characterControl.MoveVector = math.rotate(characterBody.RotationFromParent, characterControl.MoveVector);
            characterBody.RelativeVelocity = math.rotate(characterBody.RotationFromParent, characterBody.RelativeVelocity);
        }
        
        if (characterBody.IsGrounded)
        {
            // Move on ground
            float3 targetVelocity = characterControl.MoveVector * characterComponent.GroundMaxSpeed;
            CharacterControlUtilities.StandardGroundMove_Interpolated(ref characterBody.RelativeVelocity, targetVelocity, characterComponent.GroundedMovementSharpness, deltaTime, characterBody.GroundingUp, characterBody.GroundHit.Normal);

            // Jump
            if (characterControl.Jump)
            {
                CharacterControlUtilities.StandardJump(ref characterBody, characterBody.GroundingUp * characterComponent.JumpSpeed, true, characterBody.GroundingUp);
            }

            characterComponent.CurrentJumpsInAir = 0;
        }
        else
        {
            // Move in air
            float3 airAcceleration = characterControl.MoveVector * characterComponent.AirAcceleration;
            if (math.lengthsq(airAcceleration) > 0f)
            {
                float3 tmpVelocity = characterBody.RelativeVelocity;
                CharacterControlUtilities.StandardAirMove(ref characterBody.RelativeVelocity, airAcceleration, characterComponent.AirMaxSpeed, characterBody.GroundingUp, deltaTime, false);

                // Cancel air acceleration from input if we would hit a non-grounded surface (prevents air-climbing slopes at high air accelerations)
                if (characterComponent.PreventAirAccelerationAgainstUngroundedHits && _charAspect.MovementWouldHitNonGroundedObstruction(in this, ref context, ref baseContext, characterBody.RelativeVelocity * deltaTime, out ColliderCastHit hit))
                {
                    characterBody.RelativeVelocity = tmpVelocity;
                }
            }
            
            // Jump in air
            if (characterControl.Jump && characterComponent.CurrentJumpsInAir < characterComponent.MaxJumpsInAir)
            {
                CharacterControlUtilities.StandardJump(ref characterBody, characterBody.GroundingUp * characterComponent.JumpSpeed, true, characterBody.GroundingUp);
                characterComponent.CurrentJumpsInAir++;
            }
            
            // Gravity
            CharacterControlUtilities.AccelerateVelocity(ref characterBody.RelativeVelocity, characterComponent.Gravity, deltaTime);

            // Drag
            CharacterControlUtilities.ApplyDragToVelocity(ref characterBody.RelativeVelocity, deltaTime, characterComponent.AirDrag);
        }
    }


        public void VariableUpdate(ref CharacterUpdateContext _, ref KinematicCharacterUpdateContext baseContext)
        {
            ref var rwBody = ref _charAspect.CharacterBody.ValueRW;
            ref var rwCharData = ref _refCharData.ValueRW;
            ref var rwControlData = ref _refControlData.ValueRW;
            ref var charRotation = ref _charAspect.LocalTransform.ValueRW.Rotation;

            // // Add rotation from parent body to the character rotation
            // // (this is for allowing a rotating moving platform to rotate your character as well, and handle interpolation properly)
            KinematicCharacterUtilities.AddVariableRateRotationFromFixedRateRotation(ref charRotation,
                rwBody.RotationFromParent, baseContext.Time.DeltaTime, rwBody.LastPhysicsUpdateDeltaTime);

            // Rotate towards move direction
            if (math.lengthsq(rwControlData.MoveVector) > 0f)
            {
                CharacterControlUtilities.SlerpRotationTowardsDirectionAroundUp(ref charRotation,
                    baseContext.Time.DeltaTime, math.normalizesafe(rwControlData.MoveVector),
                    MathUtilities.GetUpFromRotation(charRotation), rwCharData.RotationSharpness);
            }
        }
    }
}