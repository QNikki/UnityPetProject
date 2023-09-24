using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace BG.Core
{
    public class TerrainGeneration : MonoBehaviour
    {
        public class Baker : Baker<TerrainGeneration>
        {
            public override void Bake(TerrainGeneration authoring)
            {
              //  authoring.GenerateTerrain();
                // Integrate with ECS
            }
        }

        [field: SerializeField] private TerrainGenerationConfig Config { get; set; }

        [field: SerializeField] private Terrain Target { get; set; }

        private TerrainDTO State { get; set; }

        [ContextMenu("GenerateTerrain")]
        private void GenerateTerrain()
        {
            int lowRes = (int)Config.MapResolution;
            int highRes = Target.terrainData.heightmapResolution;
            float[,] heightMap = Target.terrainData.GetHeights(0, 0, highRes, highRes);
            Vector3 heightMapScale = Target.terrainData.heightmapScale;
            

            // Create base state
            State = new TerrainDTO
            {
                LowRes = new(lowRes),
                HighRes = new(highRes),
                Heightmap = new(highRes, heightMapScale, heightMap),
                Seed = (uint)System.Guid.NewGuid().GetHashCode(),
            };

            // Generate LowRes Map
            PerformMapGeneration(State.LowRes, State.Seed);

            // Upscale LowRes Map to High
            PerformUpscale(State.LowRes, State.HighRes);
            
            // apply all heightModifiers
            PerformModifications(Config, State);
        }

        private void PerformMapGeneration(MapDTO targetMap, uint seed)
        {
            // setup space for the seed points 
            var numSeedPoints = Mathf.FloorToInt(targetMap.Size * targetMap.Size * Config.BiomSeedPointDensity);
            var biomesToSpawn = new List<byte>(numSeedPoints);

            // populate biomes to spawn based on weighting
            for (int i = 0; i < Config.NumBiomes; i++)
            {
                int numEntries = Mathf.RoundToInt(numSeedPoints * Config.Biomes[i].Weight / Config.TotalWeighting);
                Debug.Log($"Wil spawn: {numEntries}, seed points for: {Config.Biomes[i].Target.Id}");

                for (int j = 0; j < numEntries; j++)
                {
                    biomesToSpawn.Add((byte)i);
                }
            }

            //spawn th individual biomes
            Random random = new Random(seed);
            while (biomesToSpawn.Count > 0)
            {
                // pick a random seed point
                int seedPointIndex = random.NextInt(0, biomesToSpawn.Count);

                //extract the biome index
                int biomeIndex = biomesToSpawn[seedPointIndex];

                //remove seed point
                biomesToSpawn.RemoveAt(seedPointIndex);
                PerformBiome(ref random, Config.Biomes[biomeIndex].Target, targetMap);
            }

            CreateMapTexture(targetMap, nameof(PerformMapGeneration));

            void PerformBiome(ref Random random, BiomeConfig biome, MapDTO targetMap)
            {
                Vector2Int spawnLocation = new()
                    { x = random.NextInt(0, targetMap.Size), y = random.NextInt(0, targetMap.Size) };

                Queue<Vector2Int> locationQueue = new();
                locationQueue.Enqueue(spawnLocation);

                // setup visited map and target intensity map 
                var visited = new bool[targetMap.Size, targetMap.Size];
                var targetIntensity = new float[targetMap.Size, targetMap.Size];
                float startValue = random.NextFloat(biome.MinIntensity, biome.MaxIntensity);
                targetIntensity[spawnLocation.x, spawnLocation.y] = startValue;
                while (locationQueue.Count > 0)
                {
                    var biomeLocation = locationQueue.Dequeue();
                    targetMap.Biome[biomeLocation.x, biomeLocation.y] = Config.BiomeIndex(biome);
                    targetMap.Strengths[biomeLocation.x, biomeLocation.y] =
                        targetIntensity[biomeLocation.x, biomeLocation.y];
                    visited[biomeLocation.x, biomeLocation.y] = true;
                    foreach (var t in GenerationUtils.NeighbourOffsets())
                    {
                        var neighbourLocation = biomeLocation + t;
                        if (targetMap.IsLocationInvalid(neighbourLocation))
                        {
                            continue;
                        }

                        if (visited[neighbourLocation.x, neighbourLocation.y])
                        {
                            continue;
                        }

                        visited[neighbourLocation.x, neighbourLocation.y] = true;
                        var decayAmount = t.magnitude;
                        var decayRate = random.NextFloat(biome.MinDecayRate, biome.MaxDecayRate);
                        var neighbourStrength =
                            targetIntensity[biomeLocation.x, biomeLocation.y] - decayAmount * decayRate;
                        targetIntensity[neighbourLocation.x, neighbourLocation.y] = neighbourStrength;
                        if (GenerationUtils.IsStrengthInvalid(neighbourStrength))
                        {
                            continue;
                        }

                        locationQueue.Enqueue(neighbourLocation);
                    }
                }
            }
        }

        // upscale with bilinear interpolation
        private void PerformUpscale(MapDTO from, MapDTO to)
        {
            // calculate map scale
            float mapScale = (float)from.Size / to.Size;

            // calculate high res map
            for (int y = 0; y < to.Size; y++)
            {
                int lowResY = Mathf.FloorToInt(y * mapScale);
                float fractionY = y * mapScale - lowResY;
                for (int x = 0; x < to.Size; x++)
                {
                    int lowResX = Mathf.FloorToInt(x * mapScale);
                    float fractionX = x * mapScale - lowResX;
                    to.Biome[x, y] = CalculateHighResBiomeIndex(from, lowResX, lowResY, fractionX, fractionY);
                }
            }

            CreateMapTexture(to, nameof(PerformUpscale));

            byte CalculateHighResBiomeIndex(MapDTO targetMap, int lowResX, int lowResY, float fractionX,
                float fractionY)
            {
                float a = targetMap.Biome[lowResX, lowResY];
                float b = targetMap.IsIndexValid(lowResX + 1) ? targetMap.Biome[lowResX + 1, lowResY] : a;
                float c = targetMap.IsIndexValid(lowResY + 1) ? targetMap.Biome[lowResX, lowResY + 1] : a;
                float d = !targetMap.IsIndexValid(lowResX + 1) ? c :
                    !targetMap.IsIndexValid(lowResY + 1) ? b : targetMap.Biome[lowResX + 1, lowResY + 1];

                // perform bilinear filtering
                float filteredIndex = a * (1 - fractionX) * (1 - fractionY) +
                                      b * fractionX * (1 - fractionY) * c * fractionY * (1 - fractionX) +
                                      d * fractionX * fractionY;

                // build array of the possible  biomes based on the values used to interpolate 
                float[] candidates = { a, b, c, d };

                // find the neighbouring biome closest to the interpolated biome
                float target = -1;
                float targetDelta = float.MaxValue;
                foreach (var candidate in candidates)
                {
                    float delta = Mathf.Abs(filteredIndex - candidate);
                    if (delta < targetDelta)
                    {
                        targetDelta = delta;
                        target = candidate;
                    }
                }

                return (byte)Mathf.RoundToInt(target);
            }
        }

        private void PerformModifications(TerrainGenerationConfig config, TerrainDTO terrain)
        {
            // Init modifiers apply
            foreach (var modifier in config.InitModifiers)
            {
                modifier.Value.Execute(terrain);
            }

            // Biome modifiers apply
            for (int biomeId = 0; biomeId < config.NumBiomes; biomeId++)
            {
                var currBiome = config.Biomes[biomeId].Target;
                foreach (var biomeModifier in currBiome.Modifiers)
                {
                    biomeModifier.Value.Execute(terrain, biomeId, currBiome);
                }
            }

            // Post modifiers apply
            foreach (var modifier in config.PostModifiers)
            {
                modifier.Value.Execute(terrain);
            }

            Target.terrainData.SetHeights(0,0, terrain.Heightmap.Value);
        }

        private void CreateMapTexture(MapDTO targetMap, string name = "mapResolution")
        {
            var texture = new Texture2D(targetMap.Size, targetMap.Size, TextureFormat.RGB24, false);
            for (var y = 0; y < targetMap.Size; y++)
            {
                for (var x = 0; x < targetMap.Size; x++)
                {
                    var hue = targetMap.Biome[x, y] / (float)Config.NumBiomes;
                    texture.SetPixel(x, y, Color.HSVToRGB(hue, 0.75f, 0.75f));
                }
            }

            texture.Apply();
            System.IO.File.WriteAllBytes($"{name}.png", texture.EncodeToPNG());
        }
    }
}