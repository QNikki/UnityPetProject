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
        [ReadOnly] public ComponentTypeHandle<MovementComponent> MovementType;
        public ComponentTypeHandle<Translation> TranslationType;
        public ComponentTypeHandle<Velocity> VelocityType;
        public ComponentTypeHandle<Rotation> RotationType;
        public ComponentTypeHandle<InputMovement> InputMovementType;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            NativeArray<Translation> translations = batchInChunk.GetNativeArray<Translation>();
        }
    }
}