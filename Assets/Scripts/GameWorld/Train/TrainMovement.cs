using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float m_StartingSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_SpeedMultiplier;
    [SerializeField] private int m_TravelScore;

    private TrackPlacement m_TrackPlacement;
    private int m_CurrentTrackIndex; // Going to which track index
    private Vector3 m_TargetTrackPos; // Going to which track index
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

    public void ChangeTrainSpeed(float speedChange, float changeDuration)
    {
        float speedTarget = m_CurrentSpeed + speedChange;

        StartCoroutine(GradualChangeSpeed(speedTarget, changeDuration));
    }

    private IEnumerator GradualChangeSpeed(float speedTarget, float changeDuration)
    {
        float time = changeDuration;

        while (time >= 0)
        {
            m_CurrentSpeed=Mathf.Lerp(m_CurrentSpeed, speedTarget, 1f - (time / changeDuration));

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
            m_TargetTrackPos = transform.position + (transform.forward * 2);
            m_NoTrackLose = true;
        }
        else
        {
            m_TargetTrack = m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex];
            m_TargetTrackPos = m_TargetTrack.transform.position;
            m_CurrentSpeed = m_CurrentSpeed < m_MaxSpeed ? m_CurrentSpeed * m_SpeedMultiplier : m_MaxSpeed;
            UXManager.Instance?.GameUI.UpdateSpeed(m_CurrentSpeed);
            m_Train.Score.AddScoreFunc(m_TravelScore);
        }

        //Maintain same height
        m_TargetTrackPos = new Vector3(m_TargetTrackPos.x, transform.position.y, m_TargetTrackPos.z);
    }

    private void Update()
    {
        // Just reach track position
        if (transform.position.x == m_TargetTrackPos.x && transform.position.z == m_TargetTrackPos.z)
        {
            if (m_NoTrackLose == true)
            {
                m_Train.TrainDerailed();
                return;
            }

            GetNewTrackTarget();
        }


        float currentSpeed = m_CurrentSpeed;
        // Rotation
        float angleDirection = Vector3.Angle(transform.forward, m_TargetTrack.forward);

        if (angleDirection > 1)
        {
            // Calculate the rotation needed to look at the target's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(m_TargetTrack.forward, transform.up);
            currentSpeed *= 0.3f;

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_CurrentSpeed * Time.deltaTime);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, m_TargetTrackPos, m_CurrentSpeed * Time.deltaTime);



    }
}
