using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameUI : UXBehaviour
{
    //public Label m_TotalScore;
    public Label m_TotalCollectableScore;
    public Button m_leftButton;
    public Button m_middleButton;
    public Button m_rightButton;

    private int m_CurrScore;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();
        //m_TotalScore = m_Root.Q<Label>("TotalScore");
        m_TotalCollectableScore = m_Root.Q<Label>("Point");
        m_leftButton = m_Root.Q<Button>("left_btn");
        m_middleButton = m_Root.Q<Button>("straight_btn");
        m_rightButton = m_Root.Q<Button>("right_btn");
        m_leftButton.clicked += () => LeftButton();
        m_middleButton.clicked += () => MiddleButton();
        m_rightButton.clicked += () => RightButton();

        m_CurrScore = 0;


    }
    //public void DisplayingScore(int score)
    //{
    //    m_TotalScore.text = score.ToString();

    //}

    public void CollectableScore(int score)
    {
        m_TotalCollectableScore.text = "x"+score.ToString();
        m_CurrScore = score;

    }

    public void RestartScore()
    {
        m_CurrScore = 0;
    }

    public void LeftButton()
    {
        Train.Instance.TrackPlacement.SpawnTrainTrack(TrackType.TRACK_LEFT);
    }

    public void MiddleButton()
    {
        Train.Instance.TrackPlacement.SpawnTrainTrack(TrackType.TRACK_STRAIGHT);

    }

    public void RightButton()
    {
        Train.Instance.TrackPlacement.SpawnTrainTrack(TrackType.TRACK_RIGHT);

    }





}
