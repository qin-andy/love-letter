using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        
        float CameraSpeed = 0.05f;
        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(-CameraSpeed, 0f, 0f);
        }
        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0f, 0f, -CameraSpeed);
        }
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(CameraSpeed, 0f, 0f);
        }
        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0f, 0f, CameraSpeed);
        }
    }
}
