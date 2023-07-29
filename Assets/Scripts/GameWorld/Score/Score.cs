using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Score : MonoBehaviour
{

    [SerializeField] public int m_CollectableScore = 0;
    [SerializeField] public int m_CollectedItems = 0;

    //public void AddScore(int score)
    //{
    //    m_Score += score;
    //    Debug.Log(m_Score);
    //    GameUI ui = UXManager.Instance.GameUI;
    //    ui.GetComponent<GameUI>();
    //    ui.DisplayingScore(m_Score);

    //}


    public void AddScoreFunc(int score)
    {
        m_CollectableScore += score;
        UXManager.Instance?.GameUI.UpdateTotalScore(m_CollectableScore);
    }

    public void CollectedItem()
    {
        m_CollectedItems++;
        UXManager.Instance?.GameUI.CollectableCount(m_CollectedItems);

    }

}
