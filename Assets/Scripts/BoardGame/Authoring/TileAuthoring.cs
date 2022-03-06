using UnityEngine;
using Assets.Scripts.BoardGame.Components;

namespace Assets.Scripts.BoardGame.Authoring
{
	public class TileAuthoring: MonoBehaviour
	{
		[SerializeField]
		private GameObject _prefab;

		[SerializeField]
		private TileType _type;
	}
}
