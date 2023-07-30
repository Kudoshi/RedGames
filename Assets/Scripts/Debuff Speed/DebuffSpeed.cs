using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSpeed : MonoBehaviour
{
    public float slowSpeed = 1.0f; // Adjust this value as per your debuff requirement
    public float slowDuration = 0.2f; // Adjust this value to control how long the debuff lasts
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Train"))
        {


            TrainMovement trainMovement = other.GetComponent<TrainMovement>();
            if (trainMovement != null)
            {

                trainMovement.ChangeTrainSpeed(-slowSpeed, slowDuration);
                Destroy(gameObject);
                Debug.Log("Speed Decreased");
            }
        }
    }
}
    
