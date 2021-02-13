using System.Collections.Generic;
using System.Linq;
using System.Text;
using Components;
using UnityEngine;

namespace MAzeGeneration
{


   public class Cell
   {
      public bool IsEpty { get; set; }

      public Vector2Int PositionOnMap { get; set; }

      public List<Components.Direction> Directions;
   }

   public class MazeController : MonoBehaviour
   {
      [SerializeField] private Vector2Int _sizeOfMap;

      [SerializeField] private Transform _parentLevel;

      [SerializeField] private GameObject _floor;

      [SerializeField] private GameObject _wall;

      private void Start()
      {
         GenerationMap();
      }

      private void GenerationMap()
      {
         // Create Empty map with cells posbble direction.
         Cell[,] map = SetupCellForMap(_sizeOfMap);

         // Enter point for generation.
         Vector2Int startPosition = new Vector2Int
         {
            x = Random.Range(0, _sizeOfMap.x),
            y = Random.Range(0, _sizeOfMap.y)
         };
         map[startPosition.x, startPosition.y].IsEpty = false;
         List<Cell> checkList = new List<Cell>
         {
            map[startPosition.x, startPosition.y]
         };

         // generate walls
         Vector2Int posMap = SearchNearlyDirection(ref map, startPosition);
         checkList.Add(map[posMap.x, posMap.y]);
         map[posMap.x, posMap.y].IsEpty = false;
         while (checkList.Count > 1)
         {
            Cell randomCell = checkList[Random.Range(0, checkList.Count - 1)];
            if (randomCell.Directions.Count == 0)
            {
               checkList.Remove(randomCell);
            }
            else
            {
               posMap = SearchNearlyDirection(ref map, randomCell.PositionOnMap);
               if (!checkList.Any(x => x.PositionOnMap == posMap))
               {
                  checkList.Add(map[posMap.x, posMap.y]);
                  map[posMap.x, posMap.y].IsEpty = false;
               }
            }
         }
      }

      private Vector2Int SearchNearlyDirection(ref Cell[,] mapOfCells, Vector2Int posFromMap)
      {
         List<Direction> possibleDirections = mapOfCells[posFromMap.x, posFromMap.y].Directions;
         if (possibleDirections.Count == 0)
         {
            throw new System.NullReferenceException(nameof(Cell));
         }

         int randomDirectionID = Random.Range(0, possibleDirections.Count);
         Vector2Int result = new Vector2Int();
         switch (possibleDirections[randomDirectionID])
         {
            case Direction.North:
               {
                  result.x = posFromMap.x;
                  result.y = posFromMap.y + 1;
                  break;
               }
            case Direction.South:
               {
                  result.x = posFromMap.x;
                  result.y = posFromMap.y - 1;
                  break;
               }
            case Direction.West:
               {
                  result.x = posFromMap.x + 1;
                  result.y = posFromMap.y;
                  break;
               }
            case Direction.East:
               {
                  result.x = posFromMap.x - 1;
                  result.y = posFromMap.y;
                  break;
               }
         }

         return result;
      }

      private Cell[,] SetupCellForMap(Vector2Int sizeOfMap)
      {
         Cell[,] map = new Cell[sizeOfMap.x, sizeOfMap.y];
         for (int posX = 0; posX < sizeOfMap.x; posX++)
         {
            for (int posY = 0; posY < sizeOfMap.y; posY++)
            {
               map[posX, posY] = new Cell
               {
                  IsEpty = true,
                  PositionOnMap = new Vector2Int(posX, posY),
                  Directions = new List<Direction> {
                     Direction.North, Direction.East, Direction.West, Direction.South,
                  }
               };

               GameObject floor = Instantiate(_floor, _parentLevel);
               floor.transform.position = new Vector3(posX, 0, posY);
            }
         }

         for (int posX = 0; posX < sizeOfMap.x; posX++)
         {
            map[posX, sizeOfMap.y - 1].Directions.Remove(Direction.West);
            map[posX, 0].Directions.Remove(Direction.East);
         }

         for (int posY = 0; posY < sizeOfMap.y; posY++)
         {
            map[0, posY].Directions.Remove(Direction.North);
            map[sizeOfMap.x, posY - 1].Directions.Remove(Direction.West);
         }

         return map;
      }
   }
}
