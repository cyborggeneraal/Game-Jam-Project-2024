using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public static ResourceController instance;

    [SerializeField] float countdown = 5.0f;
    float countdownT = 0.0f;
    public List<Resource> unlockedResources;

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
        updateUnlockedResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        countdownT += Time.fixedDeltaTime;
        if (countdownT >= countdown)
        {
            foreach (Planet planet in PlanetsController.instance.getAllPlanets())
            {
                planet.fillStock();
                planet.fillNeeds();
            }
            foreach (supplyLine line in SupplyLineController.instance.getAllSupplyLines())
            {
                line.addStockPlanet();
                line.removeStockPlanet();
            }
            updateAllNeeds();
            UIController.instance.updateAllInfo();
            countdownT -= countdown;
        }
    }

    public void updateUnlockedResources()
    {
        HashSet<Resource> set = new HashSet<Resource>();
        foreach (Planet planet in PlanetsController.instance.getAllPlanets())
        {
            if (planet.isDiscovered())
            {
                foreach (Resource resource in planet.resources.Keys)
                {
                    set.Add(resource);
                }
            }
        }
        unlockedResources = new List<Resource>();
        foreach (Resource resource in System.Enum.GetValues(typeof(Resource)))
        {
            if (set.Contains(resource))
            {
                unlockedResources.Add(resource);
            }
        }
    }

    public void updateAllNeeds()
    {
        foreach (Planet planet in PlanetsController.instance.getAllPlanets())
        {
            planet.needLevel++;
            if (planet.isDiscovered())
            {
                foreach (Resource resource in getNeeds(planet.needLevel))
                {
                    planet.addNeed(resource, 1);
                }
            }
        }
    }

    public HashSet<Resource> getNeeds(int level)
    {
        HashSet<Resource> result = new HashSet<Resource>();
        if (level % 5 == 0)
        {
            Dictionary<int, HashSet<Resource>> needPool = GeneratorController.instance.unlockResources;
            if (needPool.ContainsKey(level/5))
            {
                foreach (Resource resource in needPool[level/5])
                {
                    result.Add(resource);
                }
            }

            List<Resource> pool = new List<Resource>();
            for (int i = 1; i <= level/5; i++)
            {
                if (needPool.ContainsKey(i/5 - 3))
                {
                    foreach (Resource resource in needPool[i/5 - 3])
                    {
                        result.Add(resource);
                    }
                }
            }
            if (pool.Count > 0)
            {
                result.Add(pool[Random.Range(0, pool.Count)]);
            }
        }
        return result;

    }

}
