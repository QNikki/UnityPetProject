using UnityEngine;

namespace Components
{
   public enum Direction
   {
      North,
      South,
      West,
      East,
   }

   class CellComponent : MonoBehaviour
   {
      public bool IsEpty { get; set; }

      public Vector2Int PositionOnMap { get; set; }

      public System.Collections.Generic.IList<Direction> Directions;
   }
}
