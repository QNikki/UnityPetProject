using UnityEngine;

namespace Assets.Scripts.BoardGame.Components
{
	public static class TileSpec
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
	}
}
