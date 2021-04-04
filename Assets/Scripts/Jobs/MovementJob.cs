using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Components;
using Unity.Transforms;

namespace Jobs
{
    [BurstCompile]
    struct MovementJob : IJobEntityBatch
    {
        public ComponentTypeHandle<MovementComponent> MovementType;
        public ComponentTypeHandle<Translation> TranslationType;
        public ComponentTypeHandle<Rotation> RotationType;
        public float DeltaTime;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            NativeArray<Translation> translations = batchInChunk.GetNativeArray(TranslationType);
            NativeArray<Rotation> rotations = batchInChunk.GetNativeArray(RotationType);
            NativeArray<MovementComponent> movements = batchInChunk.GetNativeArray(MovementType);

            for (int batch = 0; batch < batchInChunk.Count; batch++)
            {
                Translation translation = translations[batch];
                Rotation rotation = rotations[batch];
                MovementComponent movementComp = movements[batch];

                Move(movementComp.InputMovement, movementComp, translation);
            }
        }

        private void Move(InputMovement input, MovementComponent movement, Translation translation)
        {
            float3 moveDirection = math.forward() * input.Movement.y + math.right() * input.Movement.x;
            translation.Value += moveDirection * movement.MovementSpeed * DeltaTime;
        }

    }
}