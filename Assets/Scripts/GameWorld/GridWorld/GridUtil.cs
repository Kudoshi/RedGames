using Unity.Mathematics;

public static class GridUtil
{
    public const uint PRIME1 = 73856093;
    public const uint PRIME2 = 83492791;

    /// <summary>Get a random index for a given tile position.</summary>
    public static int GetTileRandIndex(int2 tilePosition, int maxIndex)
    {
        // spatial hashing function for 2D
        return (int)(
            ((uint)tilePosition.x * PRIME1) ^
            ((uint)tilePosition.y * PRIME2) % (uint)maxIndex
        );
    }
}
