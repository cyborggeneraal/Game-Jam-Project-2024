using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class supplyLine
{
    public Planet planet_a;
    public Planet planet_b;
    public Ship type;
    public Dictionary<Resource, int> delivery_a;
    public Dictionary<Resource, int> delivery_b;


    public supplyLine(Planet new_planet_a, Planet new_planet_b, Ship ship)
    {
        planet_a = new_planet_a;
        planet_b = new_planet_b;
        type = ship;
        delivery_a = new Dictionary<Resource, int>();
        delivery_b = new Dictionary<Resource, int>();
    }

    public void addDelivery(Resource resource, int value, Line line)
    {
        switch(line)
        {
            case Line.A:
                if(delivery_a.ContainsKey(resource))
                    delivery_a[resource]++;
                else
                    delivery_a.Add(resource, value);
                break;
            case Line.B:
                if(delivery_b.ContainsKey(resource))
                    delivery_b[resource]++;
                else
                    delivery_b.Add(resource, value);
                break;
        }
    }

    public void removeDelivery(Resource resource, Line line)
    {
        switch(line)
        {
            case Line.A:
                if(delivery_a.ContainsKey(resource))
                    delivery_a[resource]--;
                else
                    throw new System.ArgumentException("This resource is empty");
                break;

            case Line.B:
                if(delivery_b.ContainsKey(resource))
                    delivery_b[resource]--;
                else
                    throw new System.ArgumentException("This resource is empty");
                break;
        }
    }

    public void addStockPlanet()
    {
        Dictionary<Resource, int> oldPlanetA = new Dictionary<Resource, int>();
        foreach(KeyValuePair<Resource, int> delivery in delivery_b)
        {
            oldPlanetA.Add(delivery.Key, planet_a.getStock(delivery.Key));
            planet_a.addStock(delivery.Key, Math.Min(delivery.Value, planet_b.getStock(delivery.Key)));
        }
        foreach(KeyValuePair<Resource, int> delivery in delivery_a)
        {
            if (!oldPlanetA.ContainsKey(delivery.Key))
            {
                oldPlanetA.Add(delivery.Key, planet_a.getStock(delivery.Key));
            }
            planet_b.addStock(delivery.Key, Math.Min(delivery.Value, oldPlanetA[delivery.Key]));
        }
    }

    public void removeStockPlanet()
    {
        foreach(KeyValuePair<Resource, int> resource in delivery_a)
        {
            planet_a.removeStock(resource.Key, resource.Value);
        }
        foreach(KeyValuePair<Resource, int> resource in delivery_b)
        {
            planet_b.removeStock(resource.Key, resource.Value);
        }
    }


}
