using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Index => this.m_Index;

    private int m_Index;
    // private MeshRenderer m_MeshRenderer;

    public void Initialize(int randIndex, TileConfig[] tileConfigs)
    {
        this.m_Index = randIndex;

        int configIndex = -1;
        for (int t = 0; t < tileConfigs.Length; t++)
        {
            if (randIndex <= tileConfigs[t].UpperBound)
            {
                 break;
            }
            configIndex = t;
        }

        if (configIndex == -1) return;

        TileConfig config = tileConfigs[configIndex];

        Transform trans = config.Pool.GetNextObject();
        trans.gameObject.SetActive(true);
        trans.position = this.transform.position + Vector3.up;
    }

    private void Awake()
    {
        // this.m_MeshRenderer = this.GetComponent<MeshRenderer>();
    }
}
