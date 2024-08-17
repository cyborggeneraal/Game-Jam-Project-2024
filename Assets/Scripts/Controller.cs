using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Planet planet_a;
    public Planet planet_b;
    public supplyLine supplyline;
    

    // Start is called before the first frame update
    void Start()
    {
        planet_a = new Planet(0, 0, 0);
        planet_b = new Planet(0, 0, 0);
        supplyline = new supplyLine(planet_a, planet_b, Ship.Wooden);
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetButtonDown("Fire1")) //ctrl l
        {
            supplyline.addDelivery(Resource.Wood, 1, Line.A);
        }
            
        if (Input.GetButtonDown("Fire2")) //alt l
        {
            supplyline.addDelivery(Resource.Wood, 1, Line.B);
        }  
        
        if (Input.GetButtonDown("Fire3")) //shift l
        {
            supplyline.removeDelivery(Resource.Wood, Line.A);
        }

        if (Input.GetButtonDown("Jump")) //space
        {
            Debug.Log("Wood A: " + supplyline.delivery_a[Resource.Wood].ToString() + " Wood B: " + supplyline.delivery_b[Resource.Wood].ToString());
        }
    }
}


