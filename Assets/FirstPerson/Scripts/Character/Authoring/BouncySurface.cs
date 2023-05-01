using Unity.Entities;
using UnityEngine;

namespace FP.Core.Character
{
    public class BouncySurface : MonoBehaviour
    {
        public float BounceEnergyMultiplier = 1f;

        public class Baker : Baker<BouncySurface>
        {
            public override void Bake(BouncySurface authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new BouncySurfaceData
                {
                    BounceEnergyMultiplier = authoring.BounceEnergyMultiplier,
                });
            }
        }
    }
}