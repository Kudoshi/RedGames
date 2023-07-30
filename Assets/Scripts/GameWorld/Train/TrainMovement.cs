using System.Collections;
using UnityEngine;
using Unity.Mathematics;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float m_StartingSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_SpeedMultiplier;
    [SerializeField] private int m_TravelScore;

    private TrackPlacement m_TrackPlacement;
    private int m_CurrentTrackIndex; // Going to which track index

    private Vector3 m_BezierA;
    private Vector3 m_BezierB;
    private Vector3 m_BezierC;
    private float m_BezierTime;

    private Transform m_TargetTrack; // Going to which track index
    private float m_CurrentSpeed;
    private bool m_NoTrackLose;

    private Train m_Train;

    private void Awake()
    {
        m_Train = GetComponent<Train>();
        m_TrackPlacement = GetComponent<TrackPlacement>();
    }

    private void Start()
    {
        m_CurrentTrackIndex = m_TrackPlacement.StartingTrackIndex;
        m_TargetTrack = m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex];
        m_CurrentSpeed = m_StartingSpeed;
    }

    public void ChangeTrainSpeed(float speedPercentage, float changeDuration)
    {
        speedPercentage = math.saturate(speedPercentage);
        float speedTarget = math.max(m_CurrentSpeed * speedPercentage, this.m_StartingSpeed);
        StartCoroutine(GradualChangeSpeed(speedTarget, changeDuration));
    }

    private IEnumerator GradualChangeSpeed(float speedTarget, float changeDuration)
    {
        float time = changeDuration;

        while (time >= 0)
        {
            m_CurrentSpeed = Mathf.Lerp(m_CurrentSpeed, speedTarget, 1f - (time / changeDuration));
            time -= Time.deltaTime;

            yield return null;
        }
    }

    private void GetNewTrackTarget()
    {
        // Set the old track as travelled
        m_TargetTrack.GetComponent<Track>().SetTrackTravelled();

        m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_TrackPlacement.m_TracksPool.Count;

        // If no new track
        if (Vector3.Distance(transform.position, m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex].transform.position) > 2.1f)
        {
            this.m_BezierA = this.transform.position;
            this.m_BezierB = this.m_BezierA;
            this.m_BezierC = transform.position + transform.forward;
            m_NoTrackLose = true;
        }
        else
        {
            m_TargetTrack = m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex];

            this.m_BezierA = this.transform.position;
            this.m_BezierB = this.m_TargetTrack.position;
            this.m_BezierC = this.m_BezierB + this.m_TargetTrack.forward * 0.5f;

            m_CurrentSpeed = m_CurrentSpeed < m_MaxSpeed ? m_CurrentSpeed * m_SpeedMultiplier : m_MaxSpeed;
            UXManager.Instance?.GameUI.UpdateSpeed(m_CurrentSpeed);
            m_Train.Score.AddScoreFunc(m_TravelScore);
        }

        //Maintain same height
        m_BezierB = new Vector3(m_BezierB.x, transform.position.y, m_BezierB.z);
        m_BezierC = new Vector3(m_BezierC.x, transform.position.y, m_BezierC.z);
    }

    private void Update()
    {
        // Just reach track position
        if (transform.position.x == m_BezierC.x && transform.position.z == m_BezierC.z)
        {
            if (m_NoTrackLose == true)
            {
                m_Train.TrainDerailed();
                return;
            }

            GetNewTrackTarget();
            this.m_BezierTime = 0.0f;
        }

        this.m_BezierTime += m_CurrentSpeed * Time.deltaTime;
        this.m_BezierTime = Mathf.Clamp(this.m_BezierTime, 0.0f, 1.0f);

        this.transform.position = QuadraticBezierUtil.GetPosition(
            this.m_BezierA, this.m_BezierB, this.m_BezierC,
            this.m_BezierTime
        );

        this.transform.forward = QuadraticBezierUtil.GetTangent(
            this.m_BezierA, this.m_BezierB, this.m_BezierC,
            this.m_BezierTime
        );

        // Rotation
        // float angleDirection = Vector3.Angle(transform.forward, m_TargetTrack.forward);

        // if (angleDirection > 1)
        // {
        //     // Calculate the rotation needed to look at the target's forward direction
        //     Quaternion targetRotation = Quaternion.LookRotation(m_TargetTrack.forward, transform.up);

        //     // Smoothly rotate towards the target rotation
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_CurrentSpeed * Time.deltaTime);
        // }

        // transform.position = Vector3.MoveTowards(transform.position, m_BezierC, m_CurrentSpeed * Time.deltaTime);
    }
}
