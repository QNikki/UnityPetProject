using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Scripts.BoardGame.Components
{
	public enum TileType 
	{
		Lake,
		Farm,
		Forest,
		Tundra,
		Factory,
		Village,
		Mountain,
	}

	[GenerateAuthoringComponent]
	public struct TileComponent: IComponentData
	{
		public int TypeId;

		public float2 NumCell;
	}
}
