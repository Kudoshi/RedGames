using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Score : MonoBehaviour
{



















    [SerializeField] public int m_CollectableScore = 0;

    //public void AddScore(int score)
    //{
    //    m_Score += score;
    //    Debug.Log(m_Score);
    //    GameUI ui = UXManager.Instance.GameUI;
    //    ui.GetComponent<GameUI>();
    //    ui.DisplayingScore(m_Score);

    //}

    public void CollectableScoreFunc(int score)
    {
       
        m_CollectableScore += score;
        Debug.Log(m_CollectableScore);
        GameUI ui = UXManager.Instance.GameUI;
        ui.GetComponent<GameUI>();
        ui.CollectableScore(m_CollectableScore);

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private class m_TotalScore
    {
    }

    private class DisplayingScore
    {
    }
}
