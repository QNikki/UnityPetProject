using Unity.Entities;
using Unity.Mathematics;

namespace BoardGame.HexGrid
{

	public struct TileComponent: IComponentData
	{
		public TileType Type;

		public float2 NumCell;
	}
}
