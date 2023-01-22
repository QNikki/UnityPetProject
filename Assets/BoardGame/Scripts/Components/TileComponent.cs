using Unity.Entities;
using Unity.Mathematics;

namespace BoardGame.Components
{

	public struct TileComponent: IComponentData
	{
		public TileType Type;

		public float2 NumCell;
	}
}
