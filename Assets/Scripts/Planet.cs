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
    public int punishment;
    public int multiplier;
    public int idle_workers;
    public Vector3 position;

    public Planet(float x, float y, float z)
    {
        resources = new Dictionary<Resource, int>();
        workers = new Dictionary<Resource, int>();
        needs = new Dictionary<Resource, int>();
        statisfaction = 100;
        stock = new Dictionary<Resource, int>();
        multipliers = new Dictionary<Resource, int>();
        punishment = 1;
        idle_workers = 0;
        position.x = x;
        position.y = y;
        position.z = z;   
    }

    //Fill
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
            if(stock.ContainsKey(worker.Key))
                stock[worker.Key] = stock[worker.Key] + (worker.Value * multipliers[worker.Key]);

            else
                stock.Add(worker.Key, (worker.Value * 1));
    }

    //Assign
    public void removeWorkersFromResource(Resource from, int n)
    {
        if(getWorkers(from) >= n)
        {
            workers[from] -= n;
        }
        else
            Debug.LogError("There are no workers for this resource");
    }

    public void addWorkersToResource(Resource to, int n)
    {
        if(workers.ContainsKey(to))
            workers[to] += n;
        else
            workers.Add(to, n);
    }

    public void assignWorker(Resource to)
    {   
        if(idle_workers > 0)
        {
            addWorkersToResource(to, 1);
            idle_workers -= 1;
        }
        else
            Debug.LogError("There are no workers for this resource");
        
    }

    public void unassignWorker(Resource from)
    {   
        removeWorkersFromResource(from, 1);
        idle_workers += 1;
    }
        
    public void resignWorker(Resource from, Resource to)
    {
        removeWorkersFromResource(from, 1);
        addWorkersToResource(to, 1);
    }

    //Get
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

    //Add
    public void addResource(Resource resource, int value)
    {
        resources.Add(resource, value);
        multipliers.Add(resource, 1);
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
        if(multipliers.ContainsKey(resource))
            multipliers[resource] = value;
        else
            multipliers.Add(resource, value);
    }
        
}
