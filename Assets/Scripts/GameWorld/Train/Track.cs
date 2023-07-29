using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private GameObject m_LeftRail;
    [SerializeField] private GameObject m_StraightRail;
    [SerializeField] private GameObject m_RightRail;

    private bool m_TrainHasTravelled;

    public bool TrainHasTravelled => m_TrainHasTravelled;

    public void DisplayTrack(TrackType trackType)
    {
        m_TrainHasTravelled = false;

        if (trackType == TrackType.TRACK_LEFT)
        {
            m_LeftRail.SetActive(true);
            m_StraightRail.SetActive(false);
            m_RightRail.SetActive(false);
        }
        else if (trackType == TrackType.TRACK_STRAIGHT)
        {
            m_LeftRail.SetActive(false);
            m_StraightRail.SetActive(true);
            m_RightRail.SetActive(false);
        }
        else if (trackType == TrackType.TRACK_RIGHT)
        {
            m_LeftRail.SetActive(false);
            m_StraightRail.SetActive(false);
            m_RightRail.SetActive(true);
        }

    }

    public void SetTrackTravelled()
    {
        m_TrainHasTravelled = true;
    }
}


