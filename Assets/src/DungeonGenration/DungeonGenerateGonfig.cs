using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Dungeon
{
   [CreateAssetMenu(fileName = "DungeonGenerateConfig", menuName = "Config of Generation Dungeon")]
   public class DungeonGenerateConfig : ScriptableObject
   {
      [SerializeField] private Vector2 _dungeonSize = new Vector2(10, 10);

      [SerializeField] private DungeonPbRef[] _dungeonPbRefs = default;

      public void GetDungeonPb(PbType typeOfPb, Vector3 coord, Transform parent)
      {
         Object gameObject = PrefabUtility.InstantiatePrefab(
            assetComponentOrGameObject: _dungeonPbRefs.FirstOrDefault(x => x.TypeOfPb == typeOfPb).AssetRef,
            parent: parent
         );

         (gameObject as GameObject).transform.position = coord;
      }

      public Vector2 GetDungonSize()
      {
         return _dungeonSize;
      }
   }
}


