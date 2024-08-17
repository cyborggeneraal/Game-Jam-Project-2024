using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGameObject : MonoBehaviour
{
    [SerializeField] Planet planet;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        Planet planet = new Planet(0, 0, 0);
        planet.addWorker(2);
        planet.addResource(Resource.Wood, 3);
        planet.addResource(Resource.Coal, 3);
        planet.assignWorker(Resource.Wood);
        planet.fillStock();
        planet.fillStock();
        index = PlanetsController.instance.addPlanet(planet, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getIndex()
    {
        return index;
    }
}
