using Assets.Scripts.BoardGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.BoardGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class HexGridSpawnSystem : SystemBase
    {
        /// <summary>
        /// BeginInitializationEntityCommandBufferSystem is used to create a command buffer which will then be played back
        /// when that barrier system executes.
        /// Though the instantiation command is recorded in the SpawnJob, it's not actually processed (or "played back")
        /// until the corresponding EntityCommandBufferSystem is updated. To ensure that the transform system has a chance
        /// to run on the newly-spawned entities before they're rendered for the first time, the HexGridSpawnSystem
        /// will use the BeginSimulationEntityCommandBufferSystem to play back its commands. This introduces a one-frame lag
        /// between recording the commands and instantiating the entities, but in practice this is usually not noticeable.
        /// </summary>
        BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate()
        {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem
                .CreateCommandBuffer()
                .AsParallelWriter();

            Entities
                .WithName("HexGridSpawnSystem")
                .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                .ForEach((Entity entity, int entityInQueryIndex, in HexGridComponent spawnerFromEntity, in LocalToWorld location) =>
                {
                    for (var x = 0; x < spawnerFromEntity.CountX; x++)
                    {
                        for (var y = 0; y < spawnerFromEntity.CountY; y++)
                        {
                            var instance = commandBuffer.Instantiate(entityInQueryIndex, spawnerFromEntity.Prefab);
                            float3 position = math.transform(location.Value, new float3(x * 1f, 0, y * 1f));
                            commandBuffer.SetComponent(entityInQueryIndex, instance, new Translation { Value = position });
                        }
                    }

                    commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                }).ScheduleParallel();

            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
