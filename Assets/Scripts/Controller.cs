using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Planet planeet;

    // Start is called before the first frame update
    void Start()
    {
        planeet = new Planet();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Wood: " + planeet.stock[Resource.Wood].ToString() + " Coal: " + planeet.stock[Resource.Coal].ToString() + " Statisfaction: " + planeet.statisfaction.ToString() + " Workers: " + planeet.idle_workers.ToString());
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


