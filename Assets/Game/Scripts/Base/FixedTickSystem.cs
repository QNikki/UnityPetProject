using Unity.Burst;
using Unity.Entities;

namespace DZM.Base
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup), OrderLast = true)]
    public partial struct FixedTickSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.HasSingleton<FixedTickComponentData>())
            {
                var singletonEntity = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponentData(singletonEntity, new FixedTickComponentData());
            }

            ref var tickData = ref SystemAPI.GetSingletonRW<FixedTickComponentData>().ValueRW;
            tickData.Tick++;
        }
    }
}