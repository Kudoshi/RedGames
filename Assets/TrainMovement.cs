using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float m_StartingSpeed;
    [SerializeField] private float m_MaxSpeed;
    [SerializeField] private float m_SpeedMultiplier;

    private TrackPlacement m_TrackPlacement;
    private int m_CurrentTrackIndex; // Going to which track index
    private Vector3 m_TargetTrackPos; // Going to which track index
    private Transform m_TargetTrack; // Going to which track index
    private float m_CurrentSpeed;
    private bool m_NoTrackLose;

    private void Awake()
    {
        m_TrackPlacement = GetComponent<TrackPlacement>();
    }

    private void Start()
    {
        m_CurrentTrackIndex = m_TrackPlacement.StartingTrackIndex;
        m_CurrentSpeed = m_StartingSpeed;
    }

    private void GetNewTrackTarget()
    {
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
                Debug.Log("DEAD");
                return;
            }

            GetNewTrackTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, m_TargetTrackPos, m_CurrentSpeed * Time.deltaTime);
        
        // Rotation
        float angleDirection = Vector3.Angle(transform.forward, m_TargetTrack.forward);

        if (angleDirection > 1)
        {
            // Calculate the rotation needed to look at the target's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(m_TargetTrack.forward, transform.up);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_CurrentSpeed * Time.deltaTime);
        }
       

    }
}
