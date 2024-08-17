using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Planet planeet;

    // Start is called before the first frame update
    void Start()
    {
        planeet = new Planet(0, 0, 0);
        planeet.addWorker(10);
        planeet.addResource(Resource.Wood, 2);
        planeet.addResource(Resource.Coal, 2);
        planeet.addNeed(Resource.Wood, 2);
        planeet.addNeed(Resource.Coal, 1);
        planeet.addMultiplier(Resource.Wood, 2);
        planeet.assignWorker(Resource.Coal);
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetButtonDown("Fire1")) //ctrl l
        {
            planeet.resignWorker(Resource.Wood, Resource.Coal);
        }
            
        if (Input.GetButtonDown("Fire2")) //alt l
        {
            planeet.unassignWorker(Resource.Wood);
        }  
        
        if (Input.GetButtonDown("Fire3")) //shift l
        {
            planeet.assignWorker(Resource.Wood);
        }

        if (Input.GetButtonDown("Jump")) //space
        {
            Debug.Log("Workers: " + planeet.idle_workers.ToString() + " Wood Workers: " + planeet.workers[Resource.Wood].ToString() + " Coal Workers: " + planeet.workers[Resource.Coal].ToString());
        }
    }
}


