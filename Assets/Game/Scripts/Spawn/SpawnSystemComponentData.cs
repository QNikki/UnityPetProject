using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace DZM.Spawn
{
    [BurstCompile]
    public struct SpawnSystemComponentData: IComponentData
    {
        public Random Random;
    }
}