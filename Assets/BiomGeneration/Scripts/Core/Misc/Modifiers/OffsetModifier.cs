using System;
using UnityEngine;

namespace BG.Core
{
    [Serializable]
    public class OffsetModifier : BaseHeightMapModifier
    {
        public override HeightmapModifierType Type => HeightmapModifierType.Offset;

        [field: SerializeField] private float OffsetAmount { get; set; }

        public override void Execute(TerrainDTO terrain, int biomeIndex = -1, BiomeConfig config = null)
        {
            var heightmap = terrain.Heightmap;
            var biomeMap = terrain.HighRes.Biome;
            for (int y = 0; y < heightmap.Size; y++)
            {
                for (int x = 0; x < heightmap.Size; x++)
                {
                    if (biomeIndex != -1 && biomeMap[x, y] != biomeIndex)
                    {
                        continue;
                    }

                    float newHeight = heightmap.Value[x, y] + (OffsetAmount / heightmap.Scale.y);
                    
                    //blend by strength
                    heightmap.Value[x, y] = Mathf.Lerp(heightmap.Value[x, y], newHeight, Strength);
                }
            }
        }
    }
}