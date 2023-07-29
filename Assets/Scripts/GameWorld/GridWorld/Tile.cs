using UnityEngine;

using Random = Unity.Mathematics.Random;

public class Tile : MonoBehaviour
{
    public int Index => this.m_Index;

    private int m_Index;
    private MeshRenderer m_MeshRenderer;

    public void Initialize(int index)
    {
        this.m_Index = index;
        MaterialPropertyBlock block = new MaterialPropertyBlock();

        // DEBUG: set a color based on index (for debug visualization onnly)
        // Random rand = Random.CreateFromIndex((uint)index);

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
