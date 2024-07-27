using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DZM.Character
{
    [BurstCompile]
    internal partial struct RotationVariableJob : IJobEntity
    {
        public ComponentLookup<LocalTransform> LocalTransformLookup;

        private void Execute(Entity entity, in CharComponentData charComponentCharComponentData)
        {
            if (!LocalTransformLookup.TryGetComponent(entity, out var characterLocalTransform))
            {
                return;
            }

            CharacterUtils.ComputeRotationFromYAngleAndUp(charComponentCharComponentData.CharacterYDegrees, math.up(), out var tmpRotation);
            characterLocalTransform.Rotation = tmpRotation;
            LocalTransformLookup[entity] = characterLocalTransform;

            if (!LocalTransformLookup.TryGetComponent(charComponentCharComponentData.ViewEntity, out var viewLocalTransform))
            {
                return;
            }

            viewLocalTransform.Rotation = CharacterUtils.CalculateLocalViewRotation(charComponentCharComponentData.ViewPitchDegrees, 0f);
            LocalTransformLookup[charComponentCharComponentData.ViewEntity] = viewLocalTransform;
        }
    }
    
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct CharRotationVariableSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI.QueryBuilder().WithAll<LocalTransform, CharComponentData>()
                .Build());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new RotationVariableJob()
            {
                LocalTransformLookup = SystemAPI.GetComponentLookup<LocalTransform>(false),
            };

            state.Dependency = job.Schedule(state.Dependency);
        }
    }
}