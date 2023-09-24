using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BG.Core
{
    [CreateAssetMenu(fileName = "TerrainConfig_", menuName = "Biome Generations/ Terrain Config")]
    public class TerrainGenerationConfig : ScriptableObject
    {
        public enum BiomeMapResolution
        {
            Size64X64 = 64,
            Size128X128 = 128,
            Size256X256 = 256,
            Size512X512 = 512,
        }

        [Serializable]
        public record WeightedBiome
        {
            [field: SerializeField] public BiomeConfig Target { get; private set; }

            [field: SerializeField, Range(0, 1f)] public float Weight { get; private set; }
        }

        [field: SerializeField] public BiomeMapResolution MapResolution { get; private set; }

        [field: SerializeField] public List<ModifierConfig> InitModifiers { get; private set; }
        
        [field: SerializeField] public List<ModifierConfig> PostModifiers { get; private set; }

        [field: SerializeField] public List<WeightedBiome> Biomes { get; private set; }

        [field: SerializeField]
        [field: Range(0, 1)]
        public float BiomSeedPointDensity { get; set; }

        public int NumBiomes => Biomes.Count;

        public float TotalWeighting => Biomes.Sum(biome => biome.Weight);

        public byte BiomeIndex(BiomeConfig config)
        {
            foreach (var biome in Biomes)
            {
                if (biome.Target == config)
                {
                    return (byte)Biomes.IndexOf(biome);
                }
            }

            return byte.MinValue;
        }
    }
}