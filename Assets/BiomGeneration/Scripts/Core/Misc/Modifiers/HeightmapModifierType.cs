using System;

namespace BG.Core
{
    public enum HeightmapModifierType
    {
        Noise,
        Offset,
        Random,
        SetValue,
        Smooth,
    }

    public static class ModifierType
    {
        public static BaseHeightMapModifier GetModifier(HeightmapModifierType type) => type switch
        {
            HeightmapModifierType.Noise => new NoiseModifier(),
            HeightmapModifierType.Offset => new OffsetModifier(),
            HeightmapModifierType.Random => new RandomModifier(),
            HeightmapModifierType.SetValue => new SetValueModifier(),
            HeightmapModifierType.Smooth => new SmoothModifier(),
            _ => throw new NotImplementedException($"Cant find any Implementation for {type}"),
        };
    }
}