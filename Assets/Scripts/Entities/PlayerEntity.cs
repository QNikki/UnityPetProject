using Components;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Physics.PhysicsStep;

namespace Entities
{
    [AddComponentMenu("PetProject / Entities / Player ")]
    internal class PlayerEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Vector2 Velocity;

        public float DegreesPerSecond;

        [Header(" Gravity force applied to the character controller body")]
        public float3 Gravity = Default.Gravity;

        [Header(" Speed of movement initiated by user input")]
        public float MovementSpeed = 2.5f;

        [Header("  Maximum speed of movement at any given time")]
        public float MaxMovementSpeed = 10.0f;

        [Header(" Speed of rotation initiated by user input")]
        public float RotationSpeed = 2.5f;

        [Header(" Speed of upwards jump initiated by user input")]
        public float JumpUpwardsSpeed = 5.0f;

        [Header(" Maximum slope angle character can overcome (in degrees)")]
        public float MaxSlope = 60.0f;

        [Header(" Maximum number of character controller solver iterations")]
        public int MaxIterations = 10;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<Translation>(entity: entity, componentData: new Translation()
            {
                Value = transform.position,
            });

            dstManager.AddComponentData<Rotation>(entity, new Rotation()
            {
                Value = transform.rotation,
            });

            dstManager.AddComponentData<MovementComponent>(entity, new MovementComponent 
            {
                MovementSpeed = MovementSpeed,
                MaxMovementSpeed = MaxMovementSpeed,
                RotationSpeed = RotationSpeed,
                JumpUpwardsSpeed = JumpUpwardsSpeed,
                MaxSlope = MaxSlope,
                MaxIterations = MaxIterations,
            });

            dstManager.AddComponentData<InputMovement>(entity: entity, componentData: new InputMovement());
            dstManager.AddComponentData<InputAbility>(entity: entity, componentData: new InputAbility());
        }
    }
}
