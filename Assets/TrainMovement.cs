using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float m_StartingSpeed;
    [SerializeField] private float m_SpeedMultiplier;

    private TrackPlacement m_TrackPlacement;
    private int m_CurrentTrackIndex; // Going to which track index
    private float m_CurrentSpeed;

    private void Awake()
    {
        m_TrackPlacement = GetComponent<TrackPlacement>();
    }

    private void Start()
    {
        m_CurrentTrackIndex = m_TrackPlacement.StartingTrackIndex;
        m_CurrentSpeed = m_StartingSpeed;
    }

    private void Update()
    {
        if (transform.position == m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex].position)
        {
            m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_TrackPlacement.m_TracksPool.Count;
            m_CurrentSpeed *= m_SpeedMultiplier;
        }

        Transform targetTrack = m_TrackPlacement.m_TracksPool.Objects[m_CurrentTrackIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetTrack.position, m_CurrentSpeed * Time.deltaTime);

        float angleDirection = Vector3.Angle(transform.forward, targetTrack.forward);

        if (angleDirection > 1)
        {
            // Calculate the rotation needed to look at the target's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(targetTrack.forward, transform.up);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_CurrentSpeed * Time.deltaTime);
        }



       

        Debug.Log(m_TrackPlacement.m_TracksPool.GetCurrentObject().position);
    }
}
