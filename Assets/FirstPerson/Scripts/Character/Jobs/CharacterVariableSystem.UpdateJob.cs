using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.CharacterController;
using Unity.Entities;

namespace FP.Core.Character
{
    public partial struct CharacterVariableSystem
    {
        [BurstCompile]
        private partial struct UpdateJob : IJobEntity, IJobEntityChunkBeginEnd
        {
            public CharacterUpdateContext Context;
            public KinematicCharacterUpdateContext BaseContext;

            private void Execute(ref CharacterAspect characterAspect)
            {
                characterAspect.VariableUpdate(ref Context, ref BaseContext);
            }

            public bool OnChunkBegin(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask,
                in v128 chunkEnabledMask)
            {
                BaseContext.EnsureCreationOfTmpCollections();
                return true;
            }

            public void OnChunkEnd(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask,
                in v128 chunkEnabledMask, bool chunkWasExecuted)
            {
            }
        }
    }
}