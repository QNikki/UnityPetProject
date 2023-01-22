using UnityEngine;
using Unity.Entities;

namespace BoardGame.Components
{
	public class HexGridAuthoring : MonoBehaviour
	{
		[field: SerializeField]
		public Vector2Int MapSize { get; private set; }

		[field: SerializeField]
		public GameObject HexPrefab { get; private set; }

		public class HexGridBaker : Baker<HexGridAuthoring>
		{
			public override void Bake(HexGridAuthoring authoring)
			{
				AddComponent(new HexGridComponent
				{ 
					CountX = authoring.MapSize.x,
					CountY = authoring.MapSize.y, 
					Prefab = GetEntity(authoring.HexPrefab)
				});
			}
		}
	}

}
