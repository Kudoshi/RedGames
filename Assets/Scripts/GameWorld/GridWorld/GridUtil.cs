using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using GameWorld.Util;

public static class GridUtil
{
    public const uint PRIME1 = 73856093;
    public const uint PRIME2 = 83492791;

    /// <summary>Get a random index for a given tile position.</summary>
    public static int GetTileRandIndex(int2 tilePosition, int maxIndex)
    {
        // spatial hashing function for 2D
        return (int)(
            (((uint)tilePosition.x * PRIME1) ^
            ((uint)tilePosition.y * PRIME2)) % (uint)maxIndex
        );
    }

    public static void UpdateTileAvailability(
        int2 center, int2 size,
        ref List<int> unusedTileIndices,
        in Tile[] tiles
    ) {
        // clear list before adding in tile indices
        unusedTileIndices.Clear();
        int2 halfSize = size / 2;

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                int tileIndex = GetTileIndex(x, y, size);
                Tile tile = tiles[tileIndex];
                int2 position = (int2)mathxx.flatten_3d(
                    tile.transform.position
                );

                if (!TileInScreen(position, center, size))
                {
                    unusedTileIndices.Add(tileIndex);
                }
            }
        }
    }

    public static void UpdateTiles(
        int2 center, int2 size, LayerMask tileLayer,
        in List<int> unusedTileIndices,
        in Tile[] tiles
    ) {
        int index = 0;
        int2 halfSize = size / 2;

        Collider[] colliders = new Collider[1];

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                int2 position = GetTilePosition(x, y, halfSize, center);
                float3 position3D = mathxx.unflatten_2d(position);

                Physics.OverlapSphereNonAlloc(
                    position3D, 0.3f, colliders, tileLayer
                );

                if (colliders[0] == null)
                {
                    int tileIndex  = unusedTileIndices[index++];
                    Tile tile = tiles[tileIndex];

                    tile.transform.position = position3D;
                    // tile.Initialize();
                }

                colliders[0] = null;
            }
        }
    }

    public static bool TileInScreen(
        int2 position, int2 center, int2 size
    ) {
        int2 halfSize = size / 2;
        int2 minBound = GetTilePosition(0, 0, halfSize, center);
        int2 maxBound = GetTilePosition(size.x, size.y, halfSize, center);

        return math.all(position >= minBound) && math.all(position < maxBound);
    }

    public static int2 GetTilePosition(
        int x, int y,
        int2 halfSize, int2 center
    ) {
        int2 position = new int2(x, y);
        return position - halfSize + center;
    }

    public static int GetTileIndex(int x, int y, int2 size)
    {
        return x + y * size.x;
    }
}
