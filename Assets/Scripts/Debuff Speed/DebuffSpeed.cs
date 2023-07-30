using UnityEngine;

public class DebuffSpeed : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float slowMultiplier = 0.8f; // Adjust this value as per your debuff requirement
    public float slowDuration = 0.2f; // Adjust this value to control how long the debuff lasts
    public ParticleSystem mudSplashEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            TrainMovement trainMovement = other.GetComponent<TrainMovement>();
            if (trainMovement != null)
            {
                trainMovement.ChangeTrainSpeed(slowMultiplier, slowDuration);
                mudSplashEffect.Play();
                gameObject.SetActive(false);
                Debug.Log("Speed Decreased");
            }
        }
    }
}
