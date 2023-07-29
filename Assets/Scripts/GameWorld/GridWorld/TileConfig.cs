using UnityEngine;
using GameWorld.Util;

[System.Serializable]
public struct TileConfig
{
    public Transform m_Parent;
    [Range(0, 100)] public int UpperBound;
    public Pool<Transform> m_Pool;
}
