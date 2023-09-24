namespace BG.Core
{
    // ReSharper disable once InconsistentNaming
    public record MapDTO(int Size)
        {
            public byte[,] Biome { get; private set; } = new byte[Size, Size];

            public float[,] Strengths { get; private set; } = new float[Size, Size];

            public int Size { get; private set; } = Size;
        }
}