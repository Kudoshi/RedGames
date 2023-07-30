using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : UXBehaviour
{
    private Label m_NewBestLabel;
    private Label m_CollectibleCountLabel;
    private Label m_ScoreLabel;
    private Label m_HighScore;
    public Button m_RestartButton;
    public Button m_QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();
        m_RestartButton = m_Root.Q<Button>("restart-btn");
        m_QuitButton = m_Root.Q<Button>("end-btn");
        m_RestartButton.clicked += () => RestartGame();
        m_QuitButton.clicked += () => EndGame();

        
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

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UXManager.Instance.SoundManager.PlayOneShot("UIButton");
        
    }

    private void EndGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        UXManager.Instance.SoundManager.PlayOneShot("UIButton");
    }
}
