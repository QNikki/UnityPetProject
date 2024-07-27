using Unity.Entities;

namespace DZM.Base
{
    public struct FixedTickComponentData : IComponentData
    {
        public uint Tick;
    }
}