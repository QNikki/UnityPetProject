using UnityEngine;

namespace BG.Core
{
    // ReSharper disable once InconsistentNaming
    public record HeightmapDTO(int Size, Vector3 Scale, float[,] Value)
    {
        public Vector3 Scale { get; private set; } = Scale;

        public float[,] Value { get; private set; } = Value;

        public int Size { get; private set; } = Size;
    }
}