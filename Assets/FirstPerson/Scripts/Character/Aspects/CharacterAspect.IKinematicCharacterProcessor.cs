using Unity.Physics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.CharacterController;
using UnityEngine;

namespace FP.Core.Character
{
    public readonly partial struct CharacterAspect : IKinematicCharacterProcessor<CharacterUpdateContext>
    {
        public void UpdateGroundingUp(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext)
        {
            ref KinematicCharacterBody characterBody = ref _charAspect.CharacterBody.ValueRW;

            _charAspect.Default_UpdateGroundingUp(ref characterBody);
        }

        public bool CanCollideWithHit(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext,
            in BasicHit hit)
        {
            if (!PhysicsUtilities.IsCollidable(hit.Material))
            {
                return false;
            }

            CharacterData characterComponent = _refCharData.ValueRO;
            if (PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.IgnoreCollisionsTag))
            {
                return false;
            }

            return true;
        }

        public bool IsGroundedOnHit(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext,
            in BasicHit hit,
            int groundingEvaluationType)
        {
            CharacterData characterComponent = _refCharData.ValueRO;

            // Ignore grounding
            if (PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.IgnoreGroundingTag))
            {
                return false;
            }

            // Ignore step handling
            if (characterComponent.StepAndSlopeHandling.StepHandling &&
                PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.IgnoreStepHandlingTag))
            {
                characterComponent.StepAndSlopeHandling.StepHandling = false;
            }

            return _charAspect.Default_IsGroundedOnHit(
                in this,
                ref context,
                ref baseContext,
                in hit,
                in characterComponent.StepAndSlopeHandling,
                groundingEvaluationType);
        }

        public void OnMovementHit(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext,
            ref KinematicCharacterHit hit,
            ref float3 remainingMovementDirection,
            ref float remainingMovementLength,
            float3 originalVelocityDirection,
            float hitDistance)
        {
            ref KinematicCharacterBody characterBody = ref _charAspect.CharacterBody.ValueRW;
            ref float3 characterPosition = ref _charAspect.LocalTransform.ValueRW.Position;
            CharacterData characterComponent = _refCharData.ValueRO;

            // Ignore step handling
            if (characterComponent.StepAndSlopeHandling.StepHandling &&
                PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.IgnoreStepHandlingTag))
            {
                characterComponent.StepAndSlopeHandling.StepHandling = false;
            }

            _charAspect.Default_OnMovementHit(
                in this,
                ref context,
                ref baseContext,
                ref characterBody,
                ref characterPosition,
                ref hit,
                ref remainingMovementDirection,
                ref remainingMovementLength,
                originalVelocityDirection,
                hitDistance,
                characterComponent.StepAndSlopeHandling.StepHandling,
                characterComponent.StepAndSlopeHandling.MaxStepHeight,
                characterComponent.StepAndSlopeHandling.CharacterWidthForStepGroundingCheck);
        }

        public void OverrideDynamicHitMasses(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext,
            ref PhysicsMass characterMass,
            ref PhysicsMass otherMass,
            BasicHit hit)
        {
            CharacterData characterComponent = _refCharData.ValueRO;
            if (PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.ZeroMassAgainstCharacterTag))
            {
                characterMass.InverseMass = 0f;
                characterMass.InverseInertia = new float3(0f);
                otherMass.InverseMass = 1f;
                otherMass.InverseInertia = new float3(1f);
            }

            if (PhysicsUtilities.HasPhysicsTag(in baseContext.PhysicsWorld, hit.RigidBodyIndex,
                    characterComponent.InfiniteMassAgainstCharacterTag))
            {
                characterMass.InverseMass = 1f;
                characterMass.InverseInertia = new float3(1f);
                otherMass.InverseMass = 0f;
                otherMass.InverseInertia = new float3(0f);
            }
        }

        public void ProjectVelocityOnHits(
            ref CharacterUpdateContext context,
            ref KinematicCharacterUpdateContext baseContext,
            ref float3 velocity,
            ref bool characterIsGrounded,
            ref BasicHit characterGroundHit,
            in DynamicBuffer<KinematicVelocityProjectionHit> velocityProjectionHits,
            float3 originalVelocityDirection)
        {
            CharacterData characterComponent = _refCharData.ValueRO;

            var latestHit = velocityProjectionHits[^1];
            if (context.BouncySurfaceLookup.HasComponent(latestHit.Entity))
            {
                BouncySurfaceData bouncySurface = context.BouncySurfaceLookup[latestHit.Entity];
                velocity = math.reflect(velocity, latestHit.Normal);
                velocity *= bouncySurface.BounceEnergyMultiplier;
            }
            else
            {
                _charAspect.Default_ProjectVelocityOnHits(
                    ref velocity,
                    ref characterIsGrounded,
                    ref characterGroundHit,
                    in velocityProjectionHits,
                    originalVelocityDirection,
                    characterComponent.StepAndSlopeHandling.ConstrainVelocityToGroundPlane);
            }
        }
    }
}