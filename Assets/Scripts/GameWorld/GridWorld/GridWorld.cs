using UnityEngine;
using GameWorld.Util;
using Unity.Mathematics;

public class GridWorld : MonoBehaviour
{
    [SerializeField] private Pool<Tile> m_TilePool;
    [SerializeField] private int2 m_Size;

    [SerializeField] private int2 m_PrevCenter;
    [SerializeField] private int2 m_CurrCenter;
    [SerializeField] private Transform m_TargetTransform;

    [SerializeField] private LayerMask m_TileMask;

    [SerializeField] private TileConfig[] m_TileConfigs;

    private int2 m_HalfSize;

    private void UpdateCenter(Vector3 center)
    {
        int2 currCenter = new int2((int)center.x, (int)center.z);

        // if target transform move out of range, regenerate tiles
        if (!this.m_CurrCenter.Equals(currCenter))
        {
            this.m_PrevCenter = this.m_CurrCenter;
            this.m_CurrCenter = currCenter;

            // set all as false
            // this.m_TilePool.SetAllObjectActive(false);

            // for (int t = 0; t < this.m_TileConfigs.Length; t++)
            // {
            //     this.m_TileConfigs[t].Pool.SetAllObjectActive(false);
            // }

            this.GenerateTileArea();
        }
    }

    private void GenerateTileArea(bool checkTileInScreen = true)
    {
        for (int y = 0; y < this.m_Size.y; y++)
        {
            for (int x = 0; x < this.m_Size.x; x++)
            {
                int2 position = new int2(x, y);
                position = position - this.m_HalfSize + this.m_CurrCenter;

                if (this.CheckTileInScreen(position) && checkTileInScreen)
                {
                    continue;
                }

                // Collider[] colliders = Physics.OverlapSphere(
                //     new Vector3(position.x, 0.0f, position.y), 1.0f,
                    
                // )

                this.SpawnTile(position);
            }
        }
    }

    private void SpawnTile(int2 position)
    {
        Tile nextTile = this.m_TilePool.GetNextObject();
        nextTile.gameObject.SetActive(true);
        nextTile.transform.position = new Vector3(position.x, 0.0f, position.y);

        if (
            math.all(position < this.m_Size) &&
            math.all(position > -this.m_Size)
        ) {
            return;
        }

        nextTile.Initialize(
            GridUtil.GetTileRandIndex(position, 100),
            this.m_TileConfigs
        );
    }

    private bool CheckTileInScreen(int2 position)
    {
        int2 maxBound = this.m_PrevCenter + this.m_HalfSize;
        int2 minBound = this.m_PrevCenter - this.m_HalfSize;

        return math.all(position >= minBound) && math.all(position <= maxBound);
    }

    private void Start()
    {
        this.m_TilePool.Initialize(this.transform);
        this.m_HalfSize = this.m_Size / 2;

        for (int t = 0; t < this.m_TileConfigs.Length; t++)
        {
            this.m_TileConfigs[t].Initialize();
        }

        this.UpdateCenter(this.m_TargetTransform.position);
        this.GenerateTileArea(false);
    }

    private void Update()
    {
        Vector3 position = this.m_TargetTransform.position;
        this.UpdateCenter(position);
    }

    private void OnValidate()
    {
        if (this.m_TileConfigs == null) return;

        for (int t = 1; t < this.m_TileConfigs.Length; t++)
        {
            this.m_TileConfigs[t].UpperBound = Mathf.Max(
              this.m_TileConfigs[t].UpperBound,
              this.m_TileConfigs[t - 1].UpperBound
            );
        }
    }

    private void OnDestroy()
    {
        this.m_TilePool.Dispose();
    }
}
