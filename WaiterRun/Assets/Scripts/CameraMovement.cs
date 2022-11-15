using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    
    private void Awake()
    {
        
    }
    void Update()
    {
        cameraSpeed = GameObject.FindWithTag("Player").GetComponent<ChracterController>().moveSpeed;
        if (FindObjectOfType<ChracterController>().yasiyor)
            transform.position += Vector3.forward * cameraSpeed * Time.deltaTime ;

        

    }

    
}
