using System;
using UnityEngine;
using Unity.Entities;
#pragma warning disable CS0618

namespace BoardGame.HexGrid
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
				var entity = GetEntity(new TransformUsageFlags());
				AddComponent(entity, new HexGridComponent
				{ 
					CountX = authoring.MapSize.x,
					CountY = authoring.MapSize.y, 
					Prefab = GetEntity(authoring.HexPrefab)
				});
			}
		}
	}

}
