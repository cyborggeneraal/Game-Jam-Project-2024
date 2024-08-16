using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLine
{
    public Planet planet_a;
    public Planet planet_b;
    public Dictionary<Resource, int> delivery_a;
    public Dictionary<Resource, int> delivery_b;


    public SupplyLine(Planet pa, Planet pb, Dictionary<Resource, int> da, Dictionary<Resource, int> db)
    {
        planet_a = pa;
        planet_b = pb;
        delivery_a = da;
        delivery_b = db;
    }
}
