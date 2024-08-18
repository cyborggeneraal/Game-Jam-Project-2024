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
        GameObject planetGameObject = Instantiate(planetPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Planet planet = getPlanetById(planetGameObject.GetComponent<PlanetGameObject>().getIndex());
        planet.addWorker(5);
        planet.addResource(Resource.Wood, 5);
        planet.addResource(Resource.Coal, 5);
        planet.assignWorker(Resource.Wood);
        planet.assignWorker(Resource.Coal);
        planet.setDiscovered();
        planetGameObject.GetComponent<PlanetGameObject>().setVariant(1);

        GameObject planetGameObject2 = Instantiate(planetPrefab, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
        Planet planet2 = getPlanetById(planetGameObject2.GetComponent<PlanetGameObject>().getIndex());
        planet2.addResource(Resource.Wood, 10);
        planet2.addResource(Resource.Wheat, 5);
    }

    public void addWorkerToResource(Resource resource)
    {
        getPlanetById(UIController.instance.getSelectedIndex()).assignWorker(resource);
    }
}
