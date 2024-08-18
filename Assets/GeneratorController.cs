using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public static GeneratorController instance;

    Dictionary<Resource, int> resourceTiers;
    public Dictionary<int, HashSet<Resource>> unlockResources;
    [SerializeField] float radiusRing = 5.0f;

    List<GameObject> planets;

    int nextN = 1;
    int ringN = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        unlockResources = new Dictionary<int, HashSet<Resource>>(){
                { 1, new HashSet<Resource>(){Resource.Wood, Resource.Coal}},
                { 2, new HashSet<Resource>(){Resource.Wheat}},
                { 3, new HashSet<Resource>(){Resource.Iron}},
                { 6, new HashSet<Resource>(){Resource.Oil}}
        };
        planets = new List<GameObject>();
        resourceTiers = new Dictionary<Resource, int>();

        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getResourceNextTier(Resource resource)
    {
        return resourceTiers.ContainsKey(resource) ? resourceTiers[resource] : 5;
    }

    public void bumpResourceTier(Resource resource)
    {
        if (resourceTiers.ContainsKey(resource))
        {
            resourceTiers[resource] += 5;
        }
        else
        {
            resourceTiers.Add(resource, 10);
        }
    }

    public HashSet<Resource> getUnlockResource(int n)
    {
        return unlockResources.ContainsKey(n) ? unlockResources[n] : new HashSet<Resource>();
    }

    public HashSet<(Resource, int)> nextPlanetResource()
    {
        HashSet<(Resource, int)> result = new HashSet<(Resource, int)>();
        List<Resource> remainingResources = new List<Resource>();
        ResourceController.instance.updateUnlockedResources();
        foreach (Resource resource in ResourceController.instance.unlockedResources)
        {
            remainingResources.Add(resource);
        }

        foreach (Resource resource in getUnlockResource(nextN))
        {
            result.Add((resource, getResourceNextTier(resource)));
            remainingResources.Remove(resource);
        }

        while (result.Count < 2)
        {
            Resource resource = remainingResources[Random.Range(0,remainingResources.Count)];
            remainingResources.Remove(resource);
            result.Add((resource, getResourceNextTier(resource)));
        }

        return result;
    }

    public void addNextPlanet()
    {
        if (planets.Count == 0)
        {
            return;
        }

        GameObject planetObject = planets[0];

        Planet planet = PlanetsController.instance.getPlanetById(planetObject.GetComponent<PlanetGameObject>().getIndex());
        foreach ((Resource, int) resourceAmountPair in nextPlanetResource())
        {
            planet.addResource(resourceAmountPair.Item1, resourceAmountPair.Item2);
            bumpResourceTier(resourceAmountPair.Item1);
        }

        planets.Remove(planetObject);
        planetObject.GetComponent<PlanetGameObject>().turnOffFog();
        nextN++;

        if (planets.Count == 0)
        {
            addNewRingOfPlanets();
        }
    }

    public void addNewRingOfPlanets()
    {
        CameraController.maxCamRadius = ringN * radiusRing;
        float randomAngle = Random.Range(0.0f, 360.0f);
        int numOfPlanets = ringN + 1;
        float radius = ringN * radiusRing;
        for (int i = 0; i < numOfPlanets; i++)
        {
            float angle = randomAngle + i * (360.0f / numOfPlanets);
            Vector3 pos = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward *  radius;
            GameObject planet = PlanetsController.instance.spawnPlanet(pos);
            planets.Add(planet);
        }
        ringN++;
    }

    public void startGame()
    {
        GameObject planetGameObject = PlanetsController.instance.spawnPlanet(Vector3.zero);
        planetGameObject.GetComponent<PlanetGameObject>().turnOffFog();
        planetGameObject.GetComponent<PlanetGameObject>().setVariant(1);
        planetGameObject.GetComponent<PlanetGameObject>().turnonSatisfaction();
        planets.Add(planetGameObject);
        addNextPlanet();
        Planet planet = PlanetsController.instance.getPlanetById(planetGameObject.GetComponent<PlanetGameObject>().getIndex());
        planet.addWorker(5);
        planet.assignWorker(Resource.Wood);
        planet.assignWorker(Resource.Coal);
        planet.setDiscovered();

        addNextPlanet();
    }
}
