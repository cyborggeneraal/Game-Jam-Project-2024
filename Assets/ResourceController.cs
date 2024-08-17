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
                if (UIController.instance.getSelectedIndex() != -1)
                {
                    UIController.instance.updateAllInfo();
                }
            }
            countdownT -= countdown;
        }
    }

    public void updateUnlockedResources()
    {
        HashSet<Resource> set = new HashSet<Resource>();
        foreach (Planet planet in PlanetsController.instance.getAllPlanets())
        {
            foreach (Resource resource in planet.resources.Keys)
            {
                set.Add(resource);
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

}
