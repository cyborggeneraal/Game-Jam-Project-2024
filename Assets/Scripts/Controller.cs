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
        supplyline.addDelivery(Resource.Wood, 1, Line.A);
        supplyline.addDelivery(Resource.Wood, 2, Line.B);
        planet_a.addStock(Resource.Wood, 1);
        planet_b.addStock(Resource.Wood, 1);
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetButtonDown("Fire1")) //ctrl l
        {
            supplyline.addStockPlanet();
        }
            
        if (Input.GetButtonDown("Fire2")) //alt l
        {
            supplyline.addStockPlanet();
        }  
        
        if (Input.GetButtonDown("Fire3")) //shift l
        {
            supplyline.removeStockPlanet();
        }

        if (Input.GetButtonDown("Jump")) //space
        {
            Debug.Log("Wood A: " + planet_a.stock[Resource.Wood].ToString() + " Wood B: " + planet_b.stock[Resource.Wood].ToString());
        }
    }
}


