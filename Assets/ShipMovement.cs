using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    float t = 0.0f;
    float dir = 1.0f;

    [SerializeField] float speed = 1.0f;

    [SerializeField] Transform planetA;
    [SerializeField] Transform planetB;

    // Start is called before the first frame update
    void Start()
    {
        float angle = Quaternion.Angle(Quaternion.identity, Quaternion.LookRotation(planetB.position - planetA.position));
        transform.eulerAngles = new Vector3(-90.0f, -angle, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime * speed * dir / Vector3.Distance(planetA.position, planetB.position);
        while (t < 0.0f || t > 1.0f)
        {
            if (t < 0.0f)
            {
                t *= -1;
                dir = 1;
                transform.rotation *= Quaternion.Euler(0.0f, 0.0f, 180.0f);
            }
            if (t > 1.0f)
            {
                t = 2 - t;
                dir = -1;
                transform.rotation *= Quaternion.Euler(0.0f, 0.0f, -180.0f);
            }
        }
        transform.position = Vector3.Lerp(planetA.position, planetB.position, t);
    }
}
