using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceController : MonoBehaviour
{
    public static ResourceController instance;

    [SerializeField] float countdown = 5.0f;
    float countdownT = 0.0f;
    public List<Resource> unlockedResources;
    [SerializeField] int speedNeed = 7;

    public int scoreTimer = 0;

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
            foreach (supplyLine line in SupplyLineController.instance.getAllSupplyLines())
            {
                line.addStockPlanet();
                line.removeStockPlanet();
            }
            foreach (Planet planet in PlanetsController.instance.getAllPlanets())
            {
                planet.fillStock();
                planet.fillNeeds();
            }
            checkGameOver();
            updateAllNeeds();
            foreach (PlanetGameObject planetObject in PlanetsController.instance.getAllPlanetGameObjects())
            {
                Planet planet = PlanetsController.instance.getPlanetById(planetObject.getIndex());
                planetObject.satisfactionUI.updateSatisfaction(planet.statisfaction);
            }
            UIController.instance.updateAllInfo();
            scoreTimer++;
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
                foreach (Resource resource in planet.needs.Keys)
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
            if (planet.isDiscovered())
            {
                planet.needLevel++;
                foreach (Resource resource in getNeeds(planet.needLevel))
                {
                    planet.addNeed(resource, 1);
                }
            }
        }
        updateUnlockedResources();
    }

    public HashSet<Resource> getNeeds(int level)
    {
        HashSet<Resource> result = new HashSet<Resource>();
        if (level % speedNeed == 0)
        {
            Dictionary<int, HashSet<Resource>> needPool = GeneratorController.instance.unlockResources;
            if (needPool.ContainsKey(level/speedNeed))
            {
                foreach (Resource resource in needPool[level/speedNeed])
                {
                    result.Add(resource);
                }
            }

            List<Resource> pool = new List<Resource>();
            for (int i = 1; i <= level/speedNeed; i++)
            {
                if (needPool.ContainsKey(i/speedNeed - 4))
                {
                    foreach (Resource resource in needPool[i/speedNeed - 4])
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

    void checkGameOver()
    {
        foreach (Planet planet in PlanetsController.instance.getAllPlanets())
        {
            if (planet.statisfaction <= 0)
            {
                gameOver();
                return;
            }
        }
    }

    void gameOver()
    {
        gameOverController.instance.gameObject.SetActive(true);
        int numberOfPlanets = 0;
        foreach (Planet planet in PlanetsController.instance.getAllPlanets())
        {
            numberOfPlanets += planet.isDiscovered() ? 1 : 0;
        }
        gameOverController.instance.gameObject.GetComponent<gameOverController>().setScore(scoreTimer * numberOfPlanets);
        SceneManager.LoadScene("MainMenu");
    }

}
