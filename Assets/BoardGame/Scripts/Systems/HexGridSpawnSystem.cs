// using System;
// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.Entities.Graphics;
// using Unity.Jobs;
// using Unity.Mathematics;
// using Unity.Rendering;
// using Unity.Transforms;
// using UnityEngine.Rendering;
//
// namespace BoardGame.HexGrid
// {
//     [BurstCompile]
// //	[UpdateInGroup(typeof(InitializationSystemGroup))]
//     public partial struct HexGridSpawnSystem : ISystem
//     {
//         private EntityQuery GridQuery;
//         private int EntityCount;
//
//         [BurstCompile]
//         public void OnCreate(ref SystemState state)
//         {
//             UnityEngine.Debug.Log("HexGridSpawnSystem: OnCreate");
//             GridQuery = SystemAPI.QueryBuilder().WithAll<HexGridComponent>().Build();
//             state.RequireForUpdate(GridQuery);
//         }
//
//         [BurstCompile]
//         public void OnUpdate(ref SystemState state)
//         {
//             UnityEngine.Debug.Log("HexGridSpawnSystem: OnUpdate");
//             var world = World.DefaultGameObjectInjectionWorld;
//             var entityManager = world.EntityManager;
//
//             EntityCommandBuffer ecbJob = new EntityCommandBuffer(Allocator.TempJob);
//
//             var filterSettings = RenderFilterSettings.Default;
//             filterSettings.ShadowCastingMode = ShadowCastingMode.Off;
//             filterSettings.ReceiveShadows = false;
//
//             var renderMeshArray = new RenderMeshArray(new[] { Material }, Meshes.ToArray());
//             var renderMeshDescription = new RenderMeshDescription
//             {
//                 FilterSettings = filterSettings,
//                 LightProbeUsage = LightProbeUsage.Off,
//             };
//
//             var prototype = entityManager.CreateEntity();
//             RenderMeshUtility.AddComponents(
//                 prototype,
//                 entityManager,
//                 renderMeshDescription,
//                 renderMeshArray,
//                 MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
//             entityManager.AddComponentData(prototype, new MaterialColor());
//
//             var bounds = new NativeArray<RenderBounds>(Meshes.Count, Allocator.TempJob);
//             for (int i = 0; i < bounds.Length; ++i)
//                 bounds[i] = new RenderBounds { Value = Meshes[i].bounds.ToAABB() };
//
//             // Spawn most of the entities in a Burst job by cloning a pre-created prototype entity,
//             // which can be either a Prefab or an entity created at run time like in this sample.
//             // This is the fastest and most efficient way to create entities at run time.
//             var spawnJob = new SpawnJob(prototype, EntityCount, ecbJob.AsParallelWriter(), ref bounds);
//             var spawnHandle = spawnJob.Schedule(EntityCount, 128);
//             bounds.Dispose(spawnHandle);
//
//             spawnHandle.Complete();
//
//             ecbJob.Playback(entityManager);
//             ecbJob.Dispose();
//             entityManager.DestroyEntity(prototype);
//         }
//
//         [BurstCompile]
//         public void OnDestroy(ref SystemState state)
//         {
//             UnityEngine.Debug.Log("HexGridSpawnSystem: OnDestroy");
//         }
//     }
// }