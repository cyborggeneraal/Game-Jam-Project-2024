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
        planeet.addWorker(1);
        planeet.addResource(Resource.Wood, 2);
        planeet.addNeeds(Resource.Wood, 1);
        planeet.assignWorker(Resource.Wood);
        planeet.fillStock();
        planeet.addMultiplier(Resource.Wood, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Wood: " + planeet.stock[Resource.Wood].ToString() + " Statisfaction: " + planeet.statisfaction.ToString() + " Workers: " + planeet.idle_workers.ToString() + " Wood Workers: " + planeet.workers[Resource.Wood].ToString());
        }
            
        if (Input.GetButtonDown("Fire2"))
        {
            planeet.assignWorker(Resource.Wood);
        }  
        
        if (Input.GetButtonDown("Fire3"))
        {
            planeet.fillNeeds();
        }

        if (Input.GetButtonDown("Jump"))
        {
            planeet.fillStock();
        }
    }

    
}


