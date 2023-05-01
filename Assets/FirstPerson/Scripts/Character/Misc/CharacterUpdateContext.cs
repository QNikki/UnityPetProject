using Unity.Entities;
using Unity.Collections;

namespace FP.Core.Character
{
    public struct CharacterUpdateContext
    {
        [ReadOnly]
        public ComponentLookup<BouncySurfaceData> BouncySurfaceLookup;

        public void OnSystemCreate(ref SystemState state)
        {
            BouncySurfaceLookup = state.GetComponentLookup<BouncySurfaceData>(true);
        }

        public void OnSystemUpdate(ref SystemState state)
        {
            BouncySurfaceLookup.Update(ref state);
        }
    }
}