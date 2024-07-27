using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DZM.Character
{
    [BurstCompile]
    [WithAll(typeof(Simulate))]
    internal partial struct RotationFixedJob : IJobEntity
    {
        private void Execute(ref LocalTransform localTransform, in CharComponentData charComponentData)
        {
            CharacterUtils.ComputeRotationFromYAngleAndUp(charComponentData.CharacterYDegrees,
                math.up(), out quaternion tmpRotation);

            localTransform.Rotation = tmpRotation;
        }
    }
    
    [BurstCompile]
    [UpdateBefore(typeof(FixedStepSimulationSystemGroup))]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct CharRotationFixedSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(SystemAPI.QueryBuilder()
                .WithAll<LocalTransform, CharComponentData>()
                .Build());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new RotationFixedJob { };
            state.Dependency = job.Schedule(state.Dependency);
        }
    }
}