using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Planet planeet;

    // Start is called before the first frame update
    void Start()
    {
        planeet = new Planet(new Dictionary<Resource, int>(){{Resource.Wood, 2}}, 3, new Dictionary<Resource, int>(){{Resource.Wood, 1}}, 1, new Dictionary<Resource, int>(){{Resource.Wood, 2}});
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Stock: " + planeet.stock[Resource.Wood].ToString() + " Needs: " + planeet.needs[Resource.Wood].ToString() + " Statisfaction: " + planeet.statisfaction.ToString());
        }
            
        if (Input.GetButtonDown("Fire2"))
        {
            
        }  
        
        if (Input.GetButtonDown("Fire3"))
        {
            planeet.Fill_Needs();
        }

        if (Input.GetButtonDown("Jump"))
        {
            planeet.Fill_Stock();
        }
    }

    
}


