using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float velocity;
    public GameObject pipeObject;
    public float y;

    void Start()
    {
        velocity = PipeController.pipeVelocity;
        Instantiate(pipeObject, transform);
        transform.position = new Vector3(4f, 0f, 0f);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x - velocity * Time.deltaTime, 
            y, 
            0f);
    }
}
