using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Components;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    class LifetimeSystem : SystemBase
    {
        private EntityCommandBufferSystem _barrier;

        protected override void OnCreate()
        {
            _barrier = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        // OnUpdate runs on the main thread.
        protected override void OnUpdate()
        {
            var commandBuffer = _barrier.CreateCommandBuffer().AsParallelWriter();
            float deltaTime = Time.DeltaTime;
            Entities.ForEach((Entity entity, int nativeThreadIndex, ref LifetimeComponent lifetime) =>
            {
                lifetime.Value -= deltaTime;
                if (lifetime.Value < 0.0f)
                {
                    commandBuffer.DestroyEntity(nativeThreadIndex, entity);
                }
            }).ScheduleParallel();

            _barrier.AddJobHandleForProducer(Dependency);
        }
    }
}
