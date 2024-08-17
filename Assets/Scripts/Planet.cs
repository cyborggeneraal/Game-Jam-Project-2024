using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Planet
{
    public Dictionary<Resource, int> resources;
    public Dictionary<Resource, int> needs;
    public Dictionary<Resource, int> stock;
    public Dictionary<Resource, int> multipliers;
    public Dictionary<Resource, int> workers;
    public (Resource, int) worker_costs;
    public int statisfaction;
    public int punishment;
    public int idle_workers;
    public Vector3 position;

    public Planet(float x, float y, float z)
    {
        resources = new Dictionary<Resource, int>();
        needs = new Dictionary<Resource, int>();
        stock = new Dictionary<Resource, int>();
        multipliers = new Dictionary<Resource, int>();
        workers = new Dictionary<Resource, int>();
        worker_costs = (Resource.Wood, 2);
        statisfaction = 100;
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
        if(idle_workers >= n)
        {
            idle_workers -= n;
        }
        else
            throw new System.ArgumentException("There are no workers for this resource");
    }

    public void addWorkersToResource(Resource to, int n)
    {
        if(resources.ContainsKey(to))
        {
            if((getWorkers(to) - resources[to]) <= n & workers.ContainsKey(to))
            {
                workers[to] += n;
            }
            else if(resources[to] > 0)
            {
                workers.Add(to, n);
            } 
            else
                throw new System.ArgumentException("This resource has reached it's limit");
        }            
        else
            throw new System.ArgumentException("This resource does not exist");
    }

    //2.2 Assign
    public void assignWorker(Resource to)
    {   
        if(resources.ContainsKey(to))
            if(getWorkers(to) < resources[to] & idle_workers > 0)
            {
                addWorkersToResource(to, 1);
                removeWorkersFromIdle(1);
            }   
            else
                throw new System.ArgumentException("This resource has reached it's limit");
        else
            throw new System.ArgumentException("This resource does not exist");   
    }

    public void unassignWorker(Resource from)
    {   
        removeWorkersFromResource(from, 1);
        idle_workers += 1;
    }
        
    public void resignWorker(Resource from, Resource to)
    {   
        if(resources.ContainsKey(from) & resources.ContainsKey(to))
            if(0 < resources[from] & getWorkers(to) < resources[to])
            {
                removeWorkersFromResource(from, 1);
                addWorkersToResource(to, 1);
            }   
            else
                throw new System.ArgumentException("This resource has reached it's limit");
        else
            throw new System.ArgumentException("This resource does not exist");
    }

    //3 properties
    public void addResource(Resource resource, int value)
    {
        resources.Add(resource, value);
        multipliers.Add(resource, 1);

        ResourceController.instance.updateUnlockedResources();
    }

    public void addNeed(Resource resource, int value)
    {
        needs.Add(resource, value);
    }

    public void addWorker(int n)
    {
        idle_workers += n;
    }

    public void addMultiplier(Resource resource, int value)
    {
        if(multipliers.ContainsKey(resource))
            multipliers[resource] = value;
        else
            multipliers.Add(resource, value);
    }

    public void addStock(Resource resource, int value)
    {
        if(stock.ContainsKey(resource))
            stock[resource] += value;
        else
            stock.Add(resource, value);
    }

    public void removeStock(Resource resource, int value)
    {
        if(stock.ContainsKey(resource))
            stock[resource] = Math.Max(stock[resource] - value, 0);
    }

    //4 buy
    public void buyWorker(int n)
    {
        if(stock[worker_costs.Item1] >= (worker_costs.Item2 * n))
        {
            stock[worker_costs.Item1] = stock[worker_costs.Item1] - (worker_costs.Item2 * n);
            addWorker(n);
        }       
    }

    public supplyLine buySupplyLine(Ship ship, Planet planet)
    {
        switch(ship)
        {
            case Ship.Wooden:
                if(getStock(Resource.Wood) >= 10 && getStock(Resource.Coal) >= 10)
                {
                    Debug.Log(getStock(Resource.Wood));
                    stock[Resource.Wood] -= 10;
                    Debug.Log(getStock(Resource.Wood));
                    stock[Resource.Coal] -= 10;
                    UIController.instance.updateAllInfo();
                    return new supplyLine(this, planet, ship);
                }
                break;
            case Ship.Iron:
                if(stock[Resource.Wood] > 2)
                {
                    stock[Resource.Wood] -= 2;
                    return new supplyLine(this, planet, ship);
                }
                break;
            case Ship.Titanium:
                if(stock[Resource.Wood] > 2)
                {
                    stock[Resource.Wood] -= 2;
                    return new supplyLine(this, planet, ship);
                }
                break;
        }

        return null;
    }
}
