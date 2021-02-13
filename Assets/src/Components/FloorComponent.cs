using UnityEngine;

namespace Components
{
   public enum FloorComponentStates 
   {
      Available,
      Busy,
   }

   internal class FloorComponent
   {
      [SerializeField] private FloorComponentStates componentStates = FloorComponentStates.Available;
      public FloorComponentStates ComponentStates => componentStates;
   }
}
