using System;
using System.Collections.Generic;
using UnityEngine;

namespace BG.Core
{
    [CreateAssetMenu(fileName = "BiomeConfig_", menuName = "Biome Generations/ Biome Config")]
    public class BiomeConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();

        [field: SerializeField] public List<ModifierConfig> Modifiers { get; private set; } = new();

        [field: SerializeField, Range(0, 1f)] public float MinIntensity { get; set; } = 0.5f;

        [field: SerializeField, Range(0, 1f)] public float MaxIntensity { get; set; } = 1f;

        [field: SerializeField, Range(0, 1f)] public float MinDecayRate { get; set; } = 0.01f;

        [field: SerializeField, Range(0, 1f)] public float MaxDecayRate { get; set; } = 0.02f;
        
    }
}