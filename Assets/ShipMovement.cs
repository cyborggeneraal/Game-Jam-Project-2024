using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    float t = 0.0f;
    float dir = 1.0f;

    [SerializeField] float speed = 10.0f;

    [SerializeField] Transform planetA;
    [SerializeField] Transform planetB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime * speed * dir / Vector3.Distance(planetA.position, planetB.position);
        Debug.Log(t);
        while (t < 0.0f || t > 1.0f)
        {
            if (t < 0.0f)
            {
                t *= -1;
                dir = 1;
                transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            if (t > 1.0f)
            {
                t = 2 - t;
                dir = -1;
                transform.rotation *= Quaternion.Euler(0.0f, -180.0f, 0.0f);
            }
        }
        transform.position = Vector3.Lerp(planetA.position, planetB.position, t);
    }
}
