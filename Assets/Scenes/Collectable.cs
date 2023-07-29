using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] public int CollectableScore = 30;
    void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.tag == "Train")
        {
            Destroy(gameObject);
            Score gameScore = collider.gameObject.GetComponent<Score>();
            gameScore.CollectedItem();
            gameScore.AddScoreFunc(CollectableScore);

        } 

    }
}
