using UnityEngine;

using Random = Unity.Mathematics.Random;

public class Tile : MonoBehaviour
{
    public int Index => this.m_Index;

    private int m_Index;
    private MeshRenderer m_MeshRenderer;

    public void Initialize(int randIndex, TileConfig[] tileConfigs)
    {
        this.m_Index = randIndex;

        TileConfig config = tileConfigs[0];
        for (int t = 1; t < tileConfigs.Length; t++)
        {
            if (randIndex < tileConfigs[t].UpperBound)
            {
                 break;
            }
            config = tileConfigs[t];
        }

        Transform trans = config.Pool.GetNextObject();
        trans.gameObject.SetActive(true);
        trans.position = this.transform.position + Vector3.up;

        // DEBUG: set a color based on index (for debug visualization onnly)
        // Random rand = Random.CreateFromIndex((uint)index);
        // MaterialPropertyBlock block = new MaterialPropertyBlock();

        // block.SetColor(
        //     "_BaseColor",
        //     new Color(rand.NextFloat(), rand.NextFloat(), rand.NextFloat())
        // );
        // this.m_MeshRenderer.SetPropertyBlock(block);
    }

    private void Awake()
    {
        this.m_MeshRenderer = this.GetComponent<MeshRenderer>();
    }
}
