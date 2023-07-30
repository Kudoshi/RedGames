using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using GameWorld.Util;

public class GridWorldProto : MonoBehaviour
{
    [SerializeField] private Tile m_TilePrefab;
    [SerializeField] private LayerMask m_TileLayer;
    [SerializeField] private int2 m_Size;
    [SerializeField] private Transform m_TargetTransform;
    [SerializeField] private int2 m_Center;

    private Tile[] m_Tiles;
    private bool[] m_TileAvailabilities;
    private List<int> m_UnusedTileIndices;

    public int2 Size => this.m_Size;
    public int2 HalfSize => this.m_Size / 2;

    private void Start()
    {
        int tileCount = this.m_Size.x * this.m_Size.y;
        this.m_Tiles = new Tile[tileCount];
        this.m_Center = (int2)mathxx.flatten_3d(this.m_TargetTransform.position);

        // initialize tiles on xy locations based on target transform as the center
        for (int y = 0; y < this.m_Size.y; y++)
        {
            for (int x = 0; x < this.m_Size.x; x++)
            {
                int tileIndex = GridUtil.GetTileIndex(x, y, this.m_Size);
                int2 position = GridUtil.GetTilePosition(x, y, this.HalfSize, this.m_Center);

                this.m_Tiles[tileIndex] = Instantiate(
                    this.m_TilePrefab,
                    mathxx.unflatten_2d(position),
                    Quaternion.identity,
                    this.transform
                );
            }
        }

        // initialize with some predicted initial capacity
        this.m_UnusedTileIndices = new List<int>(this.m_Size.x + this.m_Size.y);
    }

    private void FixedUpdate()
    {
        int2 currCenter = (int2)mathxx.flatten_3d(
            this.m_TargetTransform.position
        );

        int2 centerDiff = currCenter - this.m_Center;
        if (math.lengthsq(centerDiff) > 0)
        {
            // center has changed update tiles

            GridUtil.UpdateTileAvailability(
                currCenter, this.m_Size,
                ref this.m_UnusedTileIndices,
                in this.m_Tiles
            );

            GridUtil.UpdateTiles(
                currCenter, this.m_Size, this.m_TileLayer,
                in this.m_UnusedTileIndices,
                in this.m_Tiles
            );

            this.m_Center = currCenter;
        }
    }
}
