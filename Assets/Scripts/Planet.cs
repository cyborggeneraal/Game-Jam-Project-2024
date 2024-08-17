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



    //1. Resource
    //1.1 Resource managament
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



    //1.2 Resource fulfillment
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
                stock[worker.Key] = stock[worker.Key] + getSurplus(worker.Key); //check multiplier
            else
                stock.Add(worker.Key, getSurplus(worker.Key));
    }

    //2. Workers
    //2.1 Management 
    public void removeWorkersFromResource(Resource from, int n)
    {
        if(getWorkers(from) >= n)
        {
            workers[from] -= n;
        }
        else
            throw new System.ArgumentException("There are no workers for this resource");
    }

    public void removeWorkersFromIdle(int n)
    {
        if(idle_workers > 0)
        {
            idle_workers -= n;
        }
        else
            throw new System.ArgumentException("There are no workers for this resource");
    }

    public void addWorkersToResource(Resource to, int n)
    {
        if(resources.ContainsKey(to))
            if(workers.ContainsKey(to) & resources[to] > 0)
                if(getWorkers(to) < resources[to])
                    workers[to] += n;
                else
                    throw new System.ArgumentException("This resource has reached it's limit");
            else
                workers.Add(to, n);
        else
            throw new System.ArgumentException("This resource does not exist");
    }

    //2.2 Assign
    public void assignWorker(Resource to)
    {   
        addWorkersToResource(to, 1);
        removeWorkersFromIdle(1);   
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

    //3 properties
    public void addResource(Resource resource, int value)
    {
        resources.Add(resource, value);
        multipliers.Add(resource, 1);
    }

    public void addNeed(Resource resource, int value)
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
