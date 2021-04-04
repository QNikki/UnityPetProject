﻿using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Components;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

// Systems can schedule work to run on worker threads.
// However, creating and removing Entities can only be done on the main thread to prevent race conditions.
// The system uses an EntityCommandBuffer to defer tasks that can't be done inside the Job.

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class SpawnerSystem : SystemBase
{
    // BeginInitializationEntityCommandBufferSystem is used to create a command buffer which will then be played back
    // when that barrier system executes.
    // Though the instantiation command is recorded in the SpawnJob, it's not actually processed (or "played back")
    // until the corresponding EntityCommandBufferSystem is updated. To ensure that the transform system has a chance
    // to run on the newly-spawned entities before they're rendered for the first time, the SpawnerSystem
    // will use the BeginSimulationEntityCommandBufferSystem to play back its commands. This introduces a one-frame lag
    // between recording the commands and instantiating the entities, but in practice this is usually not noticeable.
    BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

    protected override void OnCreate()
    {
        // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
        _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        //Instead of performing structural changes directly, a Job can add a command to an EntityCommandBuffer to perform such changes on the main thread after the Job has finished.
        //Command buffers allow you to perform any, potentially costly, calculations on a worker thread, while queuing up the actual insertions and deletions for later.
        var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        // Schedule the Entities.ForEach lambda job that will add Instantiate commands to the EntityCommandBuffer.
        // Since this job only runs on the first frame, we want to ensure Burst compiles it before running to get the best performance (3rd parameter of WithBurst)
        // The actual job will be cached once it is compiled (it will only get Burst compiled once).
        Entities
            .WithName("SpawnerSystem")
            .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
            .ForEach((Entity entity, int entityInQueryIndex, in Components.SpawnerComponent spawnerFromEntity, in LocalToWorld location) =>
            {
                var random = new Random(1);
                for (var x = 0; x < spawnerFromEntity.CountX; x++)
                {
                    for (var y = 0; y < spawnerFromEntity.CountY; y++)
                    {
                        var instance = commandBuffer.Instantiate(entityInQueryIndex, spawnerFromEntity.Prfb);

                        // Place the instantiated in a grid with some noise
                        var position = math.transform(location.Value,
                            new float3(x * 1.3F, noise.cnoise(new float2(x, y) * 0.21F) * 2, y * 1.3F));

                        commandBuffer.SetComponent<LifetimeComponent>(entityInQueryIndex, instance, new LifetimeComponent() 
                        { 
                            Value = random.NextFloat(10.0F, 100.0F) ,
                        });

                        commandBuffer.SetComponent<VelocityComponent>(entityInQueryIndex, instance, new VelocityComponent()
                        {
                            AngularVelocity = math.radians(random.NextFloat(25.0F, 90.0F)) 
                        });

                        commandBuffer.SetComponent<Translation>(entityInQueryIndex, instance, new Translation() 
                        { 
                            Value = position 
                        });
                    }
                }

                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }).ScheduleParallel();

        // SpawnJob runs in parallel with no sync point until the barrier system executes.
        // When the barrier system executes we want to complete the SpawnJob and then play back the commands (Creating the entities and placing them).
        // We need to tell the barrier system which job it needs to complete before it can play back the commands.
        _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}
