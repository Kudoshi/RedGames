using Unity.VisualScripting;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameUI : UXBehaviour
{
    //public Label m_TotalScore;
    public Label m_TotalCollectableCount;
    public Label m_TotalScore;
    public Label m_SpeedLabel;
    public Button m_leftButton;
    public Button m_middleButton;
    public Button m_rightButton;

    private int m_CurrScore;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();
        //m_TotalScore = m_Root.Q<Label>("TotalScore");
        m_TotalCollectableCount = m_Root.Q<Label>("CollectibleCount");
        m_TotalScore = m_Root.Q<Label>("TotalScore");
        m_SpeedLabel = m_Root.Q<Label>("SpeedLabel");
        m_leftButton = m_Root.Q<Button>("left_btn");
        m_middleButton = m_Root.Q<Button>("straight_btn");
        m_rightButton = m_Root.Q<Button>("right_btn");
        m_leftButton.clicked += () => LeftButton();
        m_middleButton.clicked += () => MiddleButton();
        m_rightButton.clicked += () => RightButton();

        m_CurrScore = 0;


    }
    

    public void CollectableCount(int score)
    {
        m_TotalCollectableCount.text = "x"+score.ToString();
        m_CurrScore = score;

    }

    public void UpdateTotalScore(int score)
    {

    }
    

    public void UpdateSpeed(int speed)
    {
        m_SpeedLabel.text = speed + " km/h";
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
