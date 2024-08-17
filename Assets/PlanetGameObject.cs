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
        Planet planet = new Planet(new Dictionary<Resource, int>(), new Dictionary<Resource,int>(), 1);
        planet.Fill_Stock();
        index = PlanetsController.instance.addPlanet(planet);
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
