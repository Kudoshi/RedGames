using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //[SerializeField] float rotate = 5f;
    float rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotate, 0 * Time.deltaTime);

    }
}
