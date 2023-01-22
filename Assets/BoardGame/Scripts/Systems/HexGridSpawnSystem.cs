
using BoardGame.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace BoardGame.Systems
{
	[BurstCompile]
//	[UpdateInGroup(typeof(InitializationSystemGroup))]
	public partial struct HexGridSpawnSystem : ISystem
	{
		private EntityQuery GridQuery;

		[BurstCompile]
		public void OnCreate(ref SystemState state)
		{
			UnityEngine.Debug.Log("HexGridSpawnSystem: OnCreate");
			GridQuery = SystemAPI.QueryBuilder().WithAll<HexGridComponent>().Build();
			state.RequireForUpdate(GridQuery);
		}

		[BurstCompile]
		public void OnDestroy(ref SystemState state)
		{
			UnityEngine.Debug.Log("HexGridSpawnSystem: OnDestroy");
		}

		[BurstCompile]
		public void OnUpdate(ref SystemState state)
		{
			UnityEngine.Debug.Log("HexGridSpawnSystem: OnUpdate");
			var grids = GridQuery.ToComponentDataArray<HexGridComponent>(Allocator.Temp);
			var gridEntities = GridQuery.ToEntityArray(Allocator.Temp);
			for (int i = 0; i < grids.Length; i++)
			{
				var hexes = state.EntityManager.Instantiate(grids[i].Prefab, grids[i].CountX * grids[i].CountY, Allocator.Temp);
				float2 hexIndex = new(0, 0);
				foreach (var hex in hexes)
				{

					float3 position = new(hexIndex.x * 1f, 0, hexIndex.y * 1f);
					state.EntityManager.AddComponent<TileComponent>(hex);
					SystemAPI.SetComponent(hex, new TileComponent { Type = TileType.Tundra, NumCell = hexIndex });
					var transform = SystemAPI.GetAspectRW<TransformAspect>(hex);
					transform.LocalPosition = position;
					if (hexIndex.x + 1 == grids[i].CountX)
					{
						hexIndex.x = 0;
						hexIndex.y++;
					}
					else
					{
						hexIndex.x++;
					}
				}
			}

			state.EntityManager.DestroyEntity(gridEntities);
		}
	}
}