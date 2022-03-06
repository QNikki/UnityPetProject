using Unity.Entities;

namespace Assets.Scripts.BoardGame.Components
{
	internal struct HexGridComponent: IComponentData
	{
		public Entity Prefab;

		public int CountX;

		public int CountY;
	}
}
