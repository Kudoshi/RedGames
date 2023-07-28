using GameWorld.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction { BOT = 0, RIGHT = 1, TOP = 2, LEFT = 3, DEAD = -1 }
public enum TrackType { TRACK_LEFT = -1, TRACK_STRAIGHT = 0, TRACK_RIGHT = 1 }

public class TrackPlacement : MonoBehaviour
{
    public float TRACK_HEIGHT= 1.1f;

    public Pool<Transform> m_TracksPool = new Pool<Transform>();

    [SerializeField] int m_MaxTrackPlacement = 3;
    [SerializeField] private LayerMask m_TileLayer;

    [Header("Track Prefab")]
    [SerializeField] GameObject m_TrackLeftPf;
    [SerializeField] GameObject m_TrackRightPf;
    [SerializeField] GameObject m_TrackStraightPf;


    public Material m_MatLeft;
    public Material m_MatForward;
    public Material m_MatRight;

    private int m_SpawnCheckHitLayer;
    private int m_StartingTrackIndex;

    public int StartingTrackIndex => m_StartingTrackIndex;

    private void Awake()
    {
        m_TracksPool.Initialize(new GameObject("Track Parent").transform);


        Transform track0 = m_TracksPool.GetNextObject();
        track0.position = new Vector3(0, TRACK_HEIGHT, -1);
        track0.gameObject.SetActive(true);

        Transform track1 = m_TracksPool.GetNextObject();
        track1.position = new Vector3(0, TRACK_HEIGHT, 0);
        track1.gameObject.SetActive(true);
        m_StartingTrackIndex = m_TracksPool.GetCurrentIdx();

        Transform track2 = m_TracksPool.GetNextObject();
        track2.position = new Vector3(0, TRACK_HEIGHT, 1);
        track2.gameObject.SetActive(true);

        Transform track3 = m_TracksPool.GetNextObject();
        track3.position = new Vector3(0, TRACK_HEIGHT, 2);
        track3.gameObject.SetActive(true);

        Transform track4 = m_TracksPool.GetNextObject();
        track4.position = new Vector3(0, TRACK_HEIGHT, 3);
        track4.gameObject.SetActive(true);



        // Set all bits to 1
        int allLayers = ~0;

        // Turn off the bits for the following
        m_SpawnCheckHitLayer = allLayers & ~m_TileLayer.value;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnTrainTrack(TrackType.TRACK_LEFT);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnTrainTrack(TrackType.TRACK_STRAIGHT);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnTrainTrack(TrackType.TRACK_RIGHT);
        }
    }

    public void SpawnTrainTrack(TrackType trackType)
    {
        Transform oldTrack = m_TracksPool.GetCurrentObject();
        Vector3 spawnPos = oldTrack.position + oldTrack.forward;
        Transform newTrack = m_TracksPool.GetNextObject();

        // Check if tile is empty
        Collider[] hitCollider = Physics.OverlapSphere(spawnPos, 0.45f, m_SpawnCheckHitLayer);
        
        if (hitCollider.Length > 0)
        {
            Debug.Log("Unable to spawn track");
            return;
        }

        newTrack.position = spawnPos;
        newTrack.rotation = Quaternion.identity;
        newTrack.gameObject.SetActive(true); 


        float rotationY = 0;

        if (trackType == TrackType.TRACK_LEFT)
        {
            //newTrack.GetComponent<MeshRenderer>().material = m_MatLeft;
            rotationY = oldTrack.eulerAngles.y - 90;
        }
        else if (trackType == TrackType.TRACK_STRAIGHT)
        {
            rotationY = oldTrack.eulerAngles.y;
            //newTrack.GetComponent<MeshRenderer>().material = m_MatForward;

        }
        else if (trackType == TrackType.TRACK_RIGHT)
        {
            rotationY = oldTrack.eulerAngles.y + 90;
            //newTrack.GetComponent<MeshRenderer>().material = m_MatRight;

        }

        if (rotationY == 360) rotationY = 0;

        newTrack.Rotate(new Vector3(0, rotationY, 0));

        //Debug.Log(newTrack.rotation);
    }

}
