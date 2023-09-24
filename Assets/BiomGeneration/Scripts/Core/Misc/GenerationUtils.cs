using System.Collections.Generic;
using UnityEngine;

namespace BG.Core
{
    public static class GenerationUtils
    {
        public static IEnumerable<Vector2Int> NeighbourOffsets()
        {
            yield return new(0, 1);
            yield return new(0, -1);
            yield return new(1, 0);
            yield return new(-1, 0);
            yield return new(1, 1);
            yield return new(-1, -1);
            yield return new(1, -1);
            yield return new(-1, 1);
        }

        // Map utils
        public static bool IsIndexValid(this MapDTO map, int index)
        {
            return index < map.Size;
        }

        public static bool IsLocationInvalid(this MapDTO map, Vector2Int location)
        {
            return (location.x < 0 || location.y < 0 || location.x >= map.Size ||
                    location.y >= map.Size);
        }

        // Biome utils
        public static bool IsStrengthInvalid(float strength)
        {
            return strength <= 0;
        }
    }
}