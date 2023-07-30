using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject m_CrashPfx;

    private BoxCollider m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Train"))
        {
            
            m_CrashPfx.SetActive(true);
            m_Collider.enabled = false;

            collision.collider.GetComponent<Train>().TrainCrashed();
        }
    }
}
