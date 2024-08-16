using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLine
{
    public Planet planet_a;
    public Planet planet_b;
    public Dictionary<Resource, int> delivery_a;
    public Dictionary<Resource, int> delivery_b;


    public SupplyLine(Planet planet_a, planet planet_b, Dictionary<Resource, int> delivery_a, Dictionary<Resource, int> delivery_b)
    {
        planet_a = planet_a;
        planet_b = planet_b;
        delivery_a = delivery_a;
        delivery_b = delivery_b;
    }
}
