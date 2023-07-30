using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //[SerializeField] float rotate = 5f;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float initialRotate = Random.Range(0, 179);
        transform.Rotate(0, initialRotate, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);

    }
}
