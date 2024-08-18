using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_to_camera : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the direction from the object to the camera
        Vector3 direction = Camera.main.transform.position - transform.position;

        // Rotate the object to face the camera
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
