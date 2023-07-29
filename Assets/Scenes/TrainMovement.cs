using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
  
    Rigidbody rb;                          // Reference to the Rigidbody component attached to the GameObject
    [SerializeField] float sidewaysForce = 1000f;    // The force applied for sideways movement
    [SerializeField] float forwardForce = 1000f;     // The force applied for forward and backward movement
    [SerializeField] float mainThrust = 1000f;       // The force applied for jumping

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();       // Get the reference to the Rigidbody component on the same GameObject
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");    // Get the horizontal input (e.g., A/D or left/right arrow keys)
        float verticalInput = Input.GetAxis("Vertical");        // Get the vertical input (e.g., W/S or up/down arrow keys)

        // Check for right movement (D key or positive horizontal input)
        if (Input.GetKey(KeyCode.D) || horizontalInput > 0f)
        {
            //Debug.Log("Move Right");
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);   // Apply sideways force to the right
        }
        // Check for left movement (A key or negative horizontal input)
        else if (Input.GetKey(KeyCode.A) || horizontalInput < 0f)
        {
            //Debug.Log("Move Left");
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);  // Apply sideways force to the left
        }

        // Check for forward movement (W key or positive vertical input)
        if (Input.GetKey(KeyCode.W) || verticalInput > 0f)
        {
            //Debug.Log("Move Forward");
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);    // Apply forward force
        }
        // Check for backward movement (S key or negative vertical input)
        else if (Input.GetKey(KeyCode.S) || verticalInput < 0f)
        {
            //Debug.Log("Move Backward");
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime);   // Apply backward force
        }

        // Check for jump (Space key)
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Jump");
            rb.AddForce(Vector3.up * mainThrust * Time.deltaTime);  // Apply vertical force for jumping
        }
    }
}

