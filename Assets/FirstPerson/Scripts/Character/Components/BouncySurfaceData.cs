using System;
using Unity.Entities;

namespace FP.Core.Character
{
    [Serializable]
    public struct BouncySurfaceData: IComponentData
    {
        public float BounceEnergyMultiplier;
    }
}