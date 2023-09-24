using System;
using UnityEngine;

namespace BG.Core
{
    [Serializable]
    public abstract class BaseHeightMapModifier
    {
        public abstract HeightmapModifierType Type { get; }

        [field: SerializeField, UnityEngine.Range(0, 1f)]
        protected float Strength { get; set; } = 1;

        public abstract void Execute(TerrainDTO terrain, int biomeIndex = -1, BiomeConfig config = null);
    }
}