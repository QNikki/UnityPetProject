using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Unity.CharacterController;
using UnityEngine;

namespace FP.Core.Character
{
    public partial struct OrbitCameraSystem
    {
        [BurstCompile]
        public partial struct OrbitCameraJob : IJobEntity
        {
            public float DeltaTime;

            public PhysicsWorld PhysicsWorld;

            public ComponentLookup<LocalToWorld> LocalToWorldLookup;

            [ReadOnly] public ComponentLookup<CameraTargetData> CameraTargetLookup;

            [ReadOnly] public ComponentLookup<KinematicCharacterBody> KinematicCharacterBodyLookup;

            private void Execute(Entity entity, ref LocalTransform localTransform, ref OrbitCameraData camera,
                in CameraControlData controls, in DynamicBuffer<CameraIgnoreBufferData> ignoredBuffer)
            {
                // if there is a followed entity, place the camera relatively to it
                if (!LocalToWorldLookup.TryGetComponent(controls.FollowedEntity, out var targetEntityLocalToWorld))
                {
                    return;
                }

                // Select the real camera target
                if (CameraTargetLookup.TryGetComponent(controls.FollowedEntity, out var camTarget) &&
                    LocalToWorldLookup.TryGetComponent(camTarget.Target, out var camTargetLtw))
                {
                    targetEntityLocalToWorld = camTargetLtw;
                    Debug.Log("camera target setted");
                }

                // Rotation
                localTransform.Rotation = quaternion.LookRotationSafe(camera.PlanarForward, targetEntityLocalToWorld.Up);

                // Handle rotating the camera along with character's parent entity (moving platform)
                // if (camera.RotateWithCharacterParent && 
                //     KinematicCharacterBodyLookup.TryGetComponent(controls.FollowedEntity, out var characterBody))
                // {
                //     KinematicCharacterUtilities.AddVariableRateRotationFromFixedRateRotation(
                //         ref localTransform.Rotation, 
                //         characterBody.RotationFromParent, 
                //         DeltaTime,
                //         characterBody.LastPhysicsUpdateDeltaTime);
                //     
                //     camera.PlanarForward = math.normalizesafe(MathUtilities.ProjectOnPlane(
                //         MathUtilities.GetForwardFromRotation(localTransform.Rotation),
                //         targetEntityLocalToWorld.Up));
                // }

                // Yaw
                var yawAngleChange = controls.Look.x * camera.RotationSpeed;
                var yawRotation = quaternion.Euler(targetEntityLocalToWorld.Up * math.radians(yawAngleChange));
                camera.PlanarForward = math.rotate(yawRotation, camera.PlanarForward);

                // Pitch
                camera.PitchAngle += -controls.Look.y * camera.RotationSpeed;
                camera.PitchAngle = math.clamp(camera.PitchAngle, camera.MinVAngle, camera.MaxVAngle);
                var pitchRotation = quaternion.Euler(math.right() * math.radians(camera.PitchAngle));

                // Final rotation
                localTransform.Rotation =
                    quaternion.LookRotationSafe(camera.PlanarForward, targetEntityLocalToWorld.Up);
                
                localTransform.Rotation = math.mul(localTransform.Rotation, pitchRotation);
                 var cameraForward = MathUtilities.GetForwardFromRotation(localTransform.Rotation);
                //
                // // Distance input
                // var desiredDistanceMovementFromInput = controls.Zoom * camera.DistanceMovementSpeed;
                // camera.TargetDistance = math.clamp(camera.TargetDistance + desiredDistanceMovementFromInput,
                //     camera.MinDistance, camera.MaxDistance);
                //
                // camera.CurrentDistanceFromMovement = math.lerp(
                //     camera.CurrentDistanceFromMovement,
                //     camera.TargetDistance,
                //     MathUtilities.GetSharpnessInterpolant(camera.DistanceMovementSharpness, DeltaTime));

                // // Obstructions
                // if (camera.ObstructionRadius > 0f)
                // {
                //     var obstructionCheckDistance = camera.CurrentDistanceFromMovement;
                //     var collector = new CameraObstructionHitsCollector(controls.FollowedEntity, ignoredBuffer, cameraForward);
                //     PhysicsWorld.SphereCastCustom(
                //         targetEntityLocalToWorld.Position,
                //         camera.ObstructionRadius, 
                //         -cameraForward,
                //         obstructionCheckDistance,
                //         ref collector,
                //         CollisionFilter.Default,
                //         QueryInteraction.IgnoreTriggers);
                //
                //     var newObstructedDistance = obstructionCheckDistance;
                //     if (collector.NumHits > 0)
                //     {
                //         newObstructedDistance = obstructionCheckDistance * collector.ClosestHit.Fraction;
                //         // Redo cast with the interpolated body transform to prevent FixedUpdate jitter in obstruction detection
                //         if (camera.PreventFixedUpdateJitter)
                //         {
                //             var hitBody = PhysicsWorld.Bodies[collector.ClosestHit.RigidBodyIndex];
                //             if (LocalToWorldLookup.TryGetComponent(hitBody.Entity, out var hitBodyLocalToWorld))
                //             {
                //                 hitBody.WorldFromBody = new RigidTransform(
                //                     quaternion.LookRotationSafe(hitBodyLocalToWorld.Forward, hitBodyLocalToWorld.Up),
                //                     hitBodyLocalToWorld.Position);
                //
                //                 collector = new CameraObstructionHitsCollector(controls.FollowedEntity,
                //                     ignoredBuffer, cameraForward);
                //                 
                //                 hitBody.SphereCastCustom(
                //                     targetEntityLocalToWorld.Position,
                //                     camera.ObstructionRadius,
                //                     -cameraForward,
                //                     obstructionCheckDistance,
                //                     ref collector,
                //                     CollisionFilter.Default,
                //                     QueryInteraction.IgnoreTriggers);
                //
                //                 if (collector.NumHits > 0)
                //                 {
                //                     newObstructedDistance = obstructionCheckDistance * collector.ClosestHit.Fraction;
                //                 }
                //             }
                //         }
                //     }

                //     // Update current distance based on obstructed distance
                //     if (camera.CurrentDistanceFromObstruction < newObstructedDistance)
                //     {
                //         // Move outer
                //         camera.CurrentDistanceFromObstruction = math.lerp(camera.CurrentDistanceFromObstruction,
                //             newObstructedDistance,
                //             MathUtilities.GetSharpnessInterpolant(camera.ObstructionOuterSmoothingSharpness,
                //                 DeltaTime));
                //     }
                //     else if (camera.CurrentDistanceFromObstruction > newObstructedDistance)
                //     {
                //         // Move inner
                //         camera.CurrentDistanceFromObstruction = math.lerp(camera.CurrentDistanceFromObstruction,
                //             newObstructedDistance,
                //             MathUtilities.GetSharpnessInterpolant(camera.ObstructionInnerSmoothingSharpness,
                //                 DeltaTime));
                //     }
                // }
                // else
                // {
                //     camera.CurrentDistanceFromObstruction = camera.CurrentDistanceFromMovement;
                // }

                // Calculate final camera position from targetPosition + rotation + distance
                localTransform.Position = targetEntityLocalToWorld.Position + -cameraForward * camera.CurrentDistanceFromObstruction;
                // Manually calculate the LocalToWorld since this is updating after the Transform systems, and the LtW is what rendering uses
                var cameraLocalToWorld = new LocalToWorld
                {
                    Value = new float4x4(localTransform.Rotation, localTransform.Position)
                };
                
                LocalToWorldLookup[entity] = cameraLocalToWorld;
            }
        }
    }
}