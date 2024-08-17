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
<<<<<<< HEAD
        Planet planet = new Planet(0, 0, 0);
=======
        Planet planet = new Planet();
        planet.addResource(Resource.Wood, 1);
        planet.addWorker(1);
>>>>>>> caf00fba5e9875bc68fe3d560a03a4a13b0b3323
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
