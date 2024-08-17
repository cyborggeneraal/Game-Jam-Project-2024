using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour
{
    public static PlanetsController instance;

    List<Planet> planets;
    List<PlanetGameObject> planetGameObjects;

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
        planetGameObjects = new List<PlanetGameObject>();
    }

    private void Start()
    {
    }

    public int addPlanet(Planet planet, PlanetGameObject planetGameObject)
    {
        int index = planets.Count;
        planets.Add(planet);
        planetGameObjects.Add(planetGameObject);
        return index;
    }

    public Planet getPlanetById(int index)
    {
        return planets[index];
    }

    public PlanetGameObject getPlanetGameObjectById(int index)
    {
        foreach (PlanetGameObject planetGameObject in planetGameObjects)
        {
            if (planetGameObject.getIndex() == index)
            {
                return planetGameObject;
            }
        }

        return null;
    }

    public List<PlanetGameObject> getAllPlanetGameObjects()
    {
        return planetGameObjects;
    }
}
