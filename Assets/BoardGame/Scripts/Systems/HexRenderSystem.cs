// using Unity.Entities;
//
// namespace BoardGame.HexGrid
// {
// 	public partial struct HexRenderSystem : ISystem
// 	{
// 		private EntityQuery _tileQuery;
//
// 		public void OnCreate(ref SystemState state)
// 		{
// 			UnityEngine.Debug.Log("HexRenderSystem: OnCreate");
// 			_tileQuery = SystemAPI.QueryBuilder().WithAll<TileComponent>()
// 				.WithNone<HexRenderComponent>()
// 				.Build();
//
// 			state.RequireForUpdate(_tileQuery);
// 		}
//
// 		public void OnDestroy(ref SystemState state)
// 		{
// 			UnityEngine.Debug.Log("HexRenderSystem:  OnDestroy");
// 		}
//
// 		public void OnUpdate(ref SystemState state)
// 		{
// 			UnityEngine.Debug.Log("HexRenderSystem:  OnUpdate");
		//	var tiles = _tileQuery.ToComponentDataArray<HexGridComponent>(Allocator.Temp);
	//		var tilesEntities = _tileQuery.ToEntityArray(Allocator.Temp);
	//		for (int i = 0; i < tiles.Length; i++)
	//		{
	//		}
	// 	}
	// }

	//public void Triangulate(HexCell[] cells)
	//{
	//	hexMesh.Clear();
	//	vertices.Clear();
	//	triangles.Clear();
	//	for (int i = 0; i < cells.Length; i++)
	//	{
	//		Triangulate(cells[i]);
	//	}
	//	hexMesh.vertices = vertices.ToArray();
	//	hexMesh.triangles = triangles.ToArray();
	//	hexMesh.RecalculateNormals();
	//}

	//void Triangulate(HexCell cell)
	//{
	//}

	//void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
	//{
	//	int vertexIndex = vertices.Count;
	//	vertices.Add(v1);
	//	vertices.Add(v2);
	//	vertices.Add(v3);
	//	triangles.Add(vertexIndex);
	//	triangles.Add(vertexIndex + 1);
	//	triangles.Add(vertexIndex + 2);
	//}


	//void Triangulate(HexCell cell)
	//{
	//	Vector3 center = cell.transform.localPosition;
	//	AddTriangle(
	//		center,
	//		center + HexMetrics.corners[0],
	//		center + HexMetrics.corners[1]
	//	);
	//}
//}