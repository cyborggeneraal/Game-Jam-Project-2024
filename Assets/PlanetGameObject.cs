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
        Planet planet = new Planet();
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
