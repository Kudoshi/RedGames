using UnityEngine;
using GameWorld.Util;

[System.Serializable]
public struct TileConfig
{
    public Transform Parent;
    [Range(0, 100)] public int UpperBound;
    public Pool<Transform> Pool;

    public void Initialize()
    {
        this.Pool.Initialize(this.Parent);
    }
}
