using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLine
{
    public Planet planet_a;
    public Planet planet_b;
    public Dictionary<Resource, int> delivery_a;
    public Dictionary<Resource, int> delivery_b;


    public SupplyLine(Planet new_planet_a, Planet new_planet_b, Dictionary<Resource, int> new_delivery_a, Dictionary<Resource, int> new_delivery_b)
    {
        planet_a = new_planet_a;
        planet_b = new_planet_b;
        delivery_a = new_delivery_a;
        delivery_b = new_delivery_b;
    }
}
