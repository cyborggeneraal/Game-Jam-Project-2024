using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour
{
    public static PlanetsController instance;

    List<Planet> planets;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        planets = new List<Planet>();
    }

    private void Start()
    {
    }

    public int addPlanet(Planet planet)
    {
        int index = planets.Count;
        planets.Add(planet);
        return index;
    }

    public Planet getPlanetById(int index)
    {
        return planets[index];
    }
}
