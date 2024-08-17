using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsController : MonoBehaviour
{
    public static PlanetsController instance;

    List<Planet> planets;
    List<PlanetGameObject> planetGameObjects;

    [SerializeField] GameObject planetPrefab;

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
        firstPlanet();
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

    public List<Planet> getAllPlanets()
    {
        return planets;
    }

    public void firstPlanet()
    {
        GameObject planetGameObject = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity);
        Planet planet = getPlanetById(planetGameObject.GetComponent<PlanetGameObject>().getIndex());
        planet.addWorker(5);
        planet.addResource(Resource.Wood, 10);
        planet.addResource(Resource.Coal, 10);
        planet.assignWorker(Resource.Wood);
        planet.assignWorker(Resource.Coal);
        planetGameObject.GetComponent<PlanetGameObject>().setVariant(1);
    }

    public void addWorkerToResource(Resource resource)
    {
        getPlanetById(UIController.instance.getSelectedIndex()).assignWorker(resource);
    }
}
