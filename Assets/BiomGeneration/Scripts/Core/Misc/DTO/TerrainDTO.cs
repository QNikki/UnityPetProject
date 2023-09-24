namespace BG.Core
{
    // ReSharper disable once InconsistentNaming
    public record TerrainDTO
        {
            public uint Seed { get; set; }

            public MapDTO LowRes { get; set; }

            public MapDTO HighRes { get; set; }

            public HeightmapDTO Heightmap { get; set; }
        }
}