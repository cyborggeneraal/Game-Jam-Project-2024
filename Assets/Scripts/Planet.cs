using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    public Dictionary<Resource, int> resources;
    public Dictionary<Resource, int> needs;
    public Dictionary<Resource, int> stock;
    public Dictionary<Resource, int> multipliers;
    public Dictionary<Resource, int> workers;
    public int statisfaction;
    public int x;
    public int y;
    public int punishment;
    public int multiplier;

    public Planet(Dictionary<Resource, int> start_resources, Dictionary<Resource, int> start_needs, int punishment_multiplier, Dictionary<Resource, int> extra_multipliers = null)
    {
        //Set variables
        resources = start_resources;
        workers = new Dictionary<Resource, int>();
        needs = start_needs;
        statisfaction = 100;
        stock = new Dictionary<Resource, int>();
        multipliers = new Dictionary<Resource, int>();
        punishment = punishment_multiplier;
        x = 0;
        y = 0;

        //Automate startup
        foreach(KeyValuePair<Resource, int> resource in resources)
        {   
            //Set stock and workers to zero
            stock.Add(resource.Key, 0);
            workers.Add(resource.Key, 0);

            //Add relevant multipliers
            if(extra_multipliers != null & extra_multipliers.ContainsKey(resource.Key))
                multipliers[resource.Key] = extra_multipliers[resource.Key];
            else
                multipliers.Add(resource.Key, 1);
            
        }
    }

    public void Fill_Needs()
    {
        foreach(KeyValuePair<Resource, int> need in needs)
        {
            if (need.Value > stock[need.Key])
            {
                statisfaction -= ((need.Value - stock[need.Key]) * punishment);
                stock[need.Key] = 0;
            }
            else
            {
                stock[need.Key] = (stock[need.Key] - need.Value);
            }
        }
    }

    public void Fill_Stock()
    {
        foreach(KeyValuePair<Resource, int> resource in resources)
        {
            stock[resource.Key] = stock[workers.Key] + (workers.Value * multipliers[resource.Key]);
        }
    }

    public void add_worker(Resource to, int Idle)
    {
        resources[to] += 1;
        Idle -= 1;
    }

    public void delete_worker(Resource from)
    {
        resources[from] -= 1;
    }
        
    public void resign_worker(Resource from, Resource to)
    {
        resources[from] -= 1;
        resources[to] += 1;
    }


}
