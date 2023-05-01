using Unity.Mathematics;
using Unity.Collections;
using Unity.Entities;

namespace BoardGame.HexGrid
{
	internal struct HexRenderComponent : IComponentData
	{
		public NativeArray<float3> Vertices { get; set; }

		public NativeArray<int> Triangles { get; set; }
	}
}