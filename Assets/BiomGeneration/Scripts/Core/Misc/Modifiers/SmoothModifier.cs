using System;
using UnityEngine;

namespace BG.Core
{
    [Serializable]
    public class SmoothModifier : BaseHeightMapModifier
    {
        public override HeightmapModifierType Type => HeightmapModifierType.Smooth;

        [field: SerializeField] private int SmoothingKernelSize { get; set; } = 5;

        public override void Execute(TerrainDTO terrain, int biomeIndex = -1, BiomeConfig config = null)
        {
            if (config != null)
            {
                Debug.Log($"bioms not supportsd, please remove modifier from {config.name}");
                return;
            }
            
            var heightmap = terrain.Heightmap;
            int mapRes = heightmap.Size;
            float[,] smoothedHeight = new float[mapRes, mapRes];
            var biomeMap = terrain.HighRes.Biome;
            for (int y = 0; y < mapRes; y++)
            {
                for (int x = 0; x < mapRes; x++)
                {
                    smoothedHeight[x, y] = SumNeighboringValues(x, y);
                }
            }
            
            for (int y = 0; y < mapRes; y++)
            {
                for (int x = 0; x < mapRes; x++)
                {
                    //blend by strength
                    heightmap.Value[x, y] = Mathf.Lerp(heightmap.Value[x, y], smoothedHeight[x, y], Strength);
                }
            }


            float SumNeighboringValues(int x, int y)
            {
                float heightSum = 0;
                int numValues = 0;
                for (int yd = -SmoothingKernelSize; yd <= SmoothingKernelSize; yd++)
                {
                    int tempY = y + yd;
                    if (tempY < 0 || tempY >= mapRes)
                    {
                        continue;
                    }

                    for (int xd = -SmoothingKernelSize; xd <= SmoothingKernelSize; xd++)
                    {
                        int tempX = x + xd;
                        if (tempX < 0 || tempX >= mapRes)
                        {
                            continue;
                        }

                        heightSum += heightmap.Value[tempX, tempY];
                        numValues++;
                    }
                }

                return heightSum / numValues;
            }
        }
    }
}