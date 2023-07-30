using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScreen : UXBehaviour
{
    private Label m_NewBestLabel;
    private Label m_CollectibleCountLabel;
    private Label m_ScoreLabel;
    private Label m_HighScore;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();

        
        m_CollectibleCountLabel = m_Root.Q<Label>("collectible");
        m_ScoreLabel = m_Root.Q<Label>("score");

        m_NewBestLabel = m_Root.Q<Label>("new-best");
        m_NewBestLabel.visible = false;
        m_HighScore = m_Root.Q<Label>("highest-score");

    }
    
    public void DisplayEndGame(int finalScore, int totalCollectible)
    {
        int bestScore = GetHighScore();

        if (finalScore > bestScore)
        {
            SaveHighScore(finalScore);
            m_NewBestLabel.visible = true;
        }

        m_ScoreLabel.text = finalScore.ToString();
        m_HighScore.text = bestScore.ToString();
        m_CollectibleCountLabel.text = totalCollectible.ToString();

        m_Document.enabled = true;
        m_Root.visible = true;
    }

    private void SaveHighScore(int finalScore)
    {
        PlayerPrefs.SetInt("HighScore", finalScore);
    }

    private int GetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            return PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            return 0;
        }
    }
}
