using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: coördinaten in de consturctor
public class Planet
{
    public Dictionary<Resource, int> resources;
    public Dictionary<Resource, int> needs;
    public Dictionary<Resource, int> stock;
    public Dictionary<Resource, int> multipliers;
    public Dictionary<Resource, int> workers;
    public int statisfaction;
    public int punishment;
    public int multiplier;
    public int idle_workers;
    public int x;
    public int y;
    public int z;

    public Planet()
    {
        //Set variables
        resources = new Dictionary<Resource, int>();
        workers = new Dictionary<Resource, int>();
        needs = new Dictionary<Resource, int>();
        statisfaction = 100;
        stock = new Dictionary<Resource, int>();
        multipliers = new Dictionary<Resource, int>();
        punishment = 1;
        idle_workers = 0;
        x = 0;
        y = 0;
        z = 0;

        //Automate startup
        foreach(KeyValuePair<Resource, int> resource in resources)
        {   
            //Set stock and workers to zero and multipliers to 1
            stock.Add(resource.Key, 0);
            workers.Add(resource.Key, 0);
            multipliers.Add(resource.Key, 1);
        }
    }

    public void fillNeeds()
    {
        foreach(KeyValuePair<Resource, int> need in needs)
        {
            if (need.Value > stock[need.Key])
            {
                statisfaction -= ((need.Value - stock[need.Key]) * punishment);
                stock[need.Key] = 0;
            }
            else
                stock[need.Key] = (stock[need.Key] - need.Value);
        }
    }

    public void fillStock()
    {
        foreach(KeyValuePair<Resource, int> worker in workers)
            stock[worker.Key] = stock[worker.Key] + (worker.Value * multipliers[worker.Key]);
    }

    public void assignWorker(Resource to)
    {
        if(idle_workers > 0 & workers[to] < resources[to])
        {
            idle_workers -= 1;
            workers[to] += 1;
        }
            
    }

    public void unassignWorker(Resource from)
    {
        workers[from] -= 1;
        idle_workers += 1;
    }
        
    public void resignWorker(Resource from, Resource to)
    {
        workers[from] -= 1;
        workers[to] += 1;
    }

    public int getStock(Resource resource)
    {
        if (stock.ContainsKey(resource))
        {
            return stock[resource];
        }
        else
        {
            return 0;
        }
    }

    public int getSurplus(Resource resource)
    {
        if (workers.ContainsKey(resource))
        {
            int multiplier = multipliers.ContainsKey(resource) ? multipliers[resource] : 1;
            return workers[resource] * multiplier;
        }
        else
        {
            return 0;
        }
    }
    
    public int getNeeds(Resource resource)
    {
        return needs.ContainsKey(resource) ? needs[resource] : 0;
    }

    public int getWorkers(Resource resource)
    {
        return workers.ContainsKey(resource) ? workers[resource] : 0;
    }

    public void addResource(Resource resource, int value)
    {
        resources.Add(resource, value);
    }

    public void addNeeds(Resource resource, int value)
    {
        needs.Add(resource, value);
    }

    public void addWorker(int x)
    {
        idle_workers += x;
    }

    public void addMultiplier(Resource resource, int value)
    {
        multipliers.Add(resource, value);
    }
        
}
