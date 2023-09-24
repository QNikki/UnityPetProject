using System;
using Unity.Mathematics;
using UnityEngine;

namespace BG.Core
{
    [Serializable]
    public class NoiseModifier : BaseHeightMapModifier
    {
        [Serializable]
        private record PerlinSettings
        {
            [field: SerializeField] public float HeightDelta { get; private set; }

            [field: SerializeField] public float ScaleX { get; private set; } = 1f;

            [field: SerializeField] public float ScaleY { get; private set; } = 1f;
        }

        public override HeightmapModifierType Type => HeightmapModifierType.Noise;

        [field: SerializeField] private PerlinSettings Primary { get; set; }

        [field: SerializeField] private PerlinSettings Secondary { get; set; }

        [field: SerializeField, Min(1)] private int Iterations { get; set; }


        public override void Execute(TerrainDTO terrain, int biomeIndex = -1, BiomeConfig config = null)
        {
            var heightmap = terrain.Heightmap;
            var biomeMap = terrain.HighRes.Biome;
            float tempSx = Primary.ScaleX;
            float tempSy = Primary.ScaleY;
            float tempHd = Primary.HeightDelta;
            for (int i = 0; i < Iterations; i++)
            {
                for (int y = 0; y < heightmap.Size; y++)
                {
                    for (int x = 0; x < heightmap.Size; x++)
                    {
                        if (biomeIndex != -1 && biomeMap[x, y] != biomeIndex)
                        {
                            continue;
                        }

                        float noiseValue = (Mathf.PerlinNoise(x * tempSx, y * tempSy) * 2f) - 1f;
                        float newHeight = heightmap.Value[x, y] + (noiseValue * tempHd / heightmap.Scale.y);
                        // blend by strengt
                        heightmap.Value[x, y] = Mathf.Lerp(heightmap.Value[x, y], newHeight, Strength);
                    }
                }

                tempSx *= Secondary.ScaleX;
                tempSy *= Secondary.ScaleY;
                tempHd *= Secondary.HeightDelta;
            }
        }
    }
}