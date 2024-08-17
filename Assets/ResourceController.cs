using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public static ResourceController instance;

    [SerializeField] float countdown = 5.0f;
    float countdownT = 0.0f;

    // Start is called before the first frame update
    void Start()
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
                UIController.instance.updateAllInfo();
            }
            countdownT -= countdown;
        }
    }

}
