using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace BoardGame.HexGrid
{
	public static class TileUtils
	{
		public const float OuterRadius = 1f;

		public const float InnerRadius = OuterRadius * 0.866025404f;

		public static Vector3[] corners =
		{
			new Vector3(0f, 0f, OuterRadius),
			new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
			new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
			new Vector3(0f, 0f, -InnerRadius),
			new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
			new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius)
		};

		public static float3 QuadToHexGrid(this float3 quadPosition)
		{
			float2 xy = new(quadPosition.x, quadPosition.z);
			quadPosition.x = (quadPosition.x + quadPosition.z * 0.5f - (int)quadPosition.z / 2) * (InnerRadius * 2f);
			quadPosition.z *= (OuterRadius * 1.5f);
			return quadPosition;
		}
	}
}
