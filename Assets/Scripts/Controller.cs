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
        planeet.addResource(Resource.Coal, 1);
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
            planeet.fillNeeds();
        }
            
        if (Input.GetButtonDown("Fire2")) //alt l
        {
            planeet.fillStock();
        }  
        
        if (Input.GetButtonDown("Fire3")) //shift l
        {
            planeet.assignWorker(Resource.Wood);
        }

        if (Input.GetButtonDown("Jump")) //space
        {
            Debug.Log("Wood: " + planeet.stock[Resource.Wood].ToString() + " Coal: " + planeet.stock[Resource.Coal].ToString() + " Statisfaction: " + planeet.statisfaction.ToString() + " Workers: " + planeet.idle_workers.ToString() + " Wood Workers: " + planeet.workers[Resource.Wood].ToString() + " Coal Workers: " + planeet.workers[Resource.Coal].ToString());
        }
    }
}


