using GameWorld.Util;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int m_CollectableScore = 30;
    [SerializeField] private Pool<ParticleSystem> m_CollectPfx;

    private void Awake()
    {
        m_CollectPfx.Initialize(transform.parent);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Train")
        {
            // Destroy(gameObject);
            UXManager.Instance.SoundManager.PlayOneShot("Collectable");
            Score gameScore = collider.gameObject.GetComponent<Score>();
            gameScore.CollectedItem();
            gameScore.AddScoreFunc(m_CollectableScore);
            ParticleSystem pfx = m_CollectPfx.GetCurrentObject();
            pfx.transform.position = transform.position;
            pfx.Play();
            gameObject.SetActive(false);

        }

    }
}
