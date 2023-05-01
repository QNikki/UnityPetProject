using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace BoardGame.HexGrid
{
    [BurstCompile]
    public struct SpawnJob : IJobParallelFor
    {
        private Entity Prototype { get; }

        private int EntityCount { get; }

        private EntityCommandBuffer.ParallelWriter ParallelWriter { get; }
        
        private NativeArray<RenderBounds> MeshBounds { get; }

        public SpawnJob(Entity prototype, int entityCount, EntityCommandBuffer.ParallelWriter parallelWriter,
            ref NativeArray<RenderBounds> meshBounds)
        {
            Prototype = prototype;
            EntityCount = entityCount;
            ParallelWriter = parallelWriter;
            MeshBounds = meshBounds;
        }

        public void Execute(int index)
        {
            var e = ParallelWriter.Instantiate(index, Prototype);
            // Prototype has all correct components up front, can use SetComponent
            ParallelWriter.SetComponent(index, e, new LocalToWorld() {Value = float4x4.identity});
            ParallelWriter.SetComponent(index, e, new MaterialColor() {Value = ComputeColor(index)});
            
            // MeshBounds must be set according to the actual mesh for culling to work.
            ParallelWriter.SetComponent(index, e, MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
            ParallelWriter.SetComponent(index, e, MeshBounds[0]);
        }

        public float4 ComputeColor(int index)
        {
            float t = (float) index / (EntityCount - 1);
            var color = Color.HSVToRGB(t, 1, 1);
            return new float4(color.r, color.g, color.b, 1);
        }

    }
}

// var grids = GridQuery.ToComponentDataArray<HexGridComponent>(Allocator.Temp);
// var gridEntities = GridQuery.ToEntityArray(Allocator.Temp);
// for (int i = 0; i < grids.Length; i++)
// {
// 	var hexes = state.EntityManager.Instantiate(grids[i].Prefab, grids[i].CountX * grids[i].CountY, Allocator.Temp);
// 	float2 hexIndex = new(0, 0);
// 	foreach (var hex in hexes)
// 	{
// 		state.EntityManager.AddComponent<TileComponent>(hex);
// 		SystemAPI.SetComponent(hex, new TileComponent { Type = TileType.Tundra, NumCell = hexIndex });
// 		var transform = SystemAPI.GetAspectRW<TransformAspect>(hex);
// 		transform.LocalPosition = new float3(hexIndex.x, 0f, hexIndex.y).QuadToHexGrid();
// 		if (Math.Abs(hexIndex.x + 1 - grids[i].CountX) < math.EPSILON)
// 		{
// 			hexIndex.x = 0;
// 			hexIndex.y++;
// 		}
// 		else
// 		{
// 			hexIndex.x++;
// 		}
// 	}
// }
//
// state.EntityManager.DestroyEntity(gridEntities);
//