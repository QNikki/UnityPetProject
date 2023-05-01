using Unity.Entities;

namespace BoardGame.HexGrid
{
	internal struct HexGridComponent : IComponentData
	{
		public Entity Prefab { get; set; }

		public int CountX { get; set; }
		
		public int CountY { get; set; }
	}
}
