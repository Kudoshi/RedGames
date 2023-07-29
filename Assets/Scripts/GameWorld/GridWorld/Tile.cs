using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private LayerMask m_TrackLayer;

    public int Index => this.m_Index;

    private int m_Index;

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
        Vector3 tilePosition = this.transform.position + Vector3.up + config.Offset;

        // check if collectable can be placed or not
        Collectable collectable = trans.GetComponent<Collectable>();
        if (collectable != null)
        {
            Collider[] colliders = Physics.OverlapBox(
                tilePosition, Vector3.one * 0.5f, Quaternion.identity,
                this.m_TrackLayer
            );

            if (colliders.Length > 0)
            {
                Track track = colliders[0].GetComponent<Track>();
                if (track != null)
                {
                    if (track.TrainHasTravelled) return;
                }
            }
        }

        trans.gameObject.SetActive(true);
        trans.position = tilePosition;
    }
}
