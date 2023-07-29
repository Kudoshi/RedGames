using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int m_CollectableScore = 30;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Train")
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
            Score gameScore = collider.gameObject.GetComponent<Score>();
            gameScore.CollectedItem();
            gameScore.AddScoreFunc(m_CollectableScore);

        } 

    }
}
