using UnityEngine;
using Unity.Mathematics;

[System.Serializable]
public struct TileConfig
{
    [Range(0, 100)] public int UpperBound;
    public Transform TargetObject;
    public Vector3 Offset;
    [HideInInspector] public Transform[] Objects;

    public void Initialize(int2 size, Transform parent)
    {
        int objectCount = size.x * size.y;
        this.Objects = new Transform[objectCount];

        for (int o = 0; o < objectCount; o++)
        {
            this.Objects[o] = Object.Instantiate(
                TargetObject, parent
            );

            this.Objects[o].gameObject.SetActive(false);
        }
    }
}
