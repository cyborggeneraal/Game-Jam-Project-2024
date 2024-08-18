using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyLineController : MonoBehaviour
{
    public static SupplyLineController instance;

    List<supplyLine> supplyLines;
    List<ShipMovement> supplyLineGameObjects;

    [SerializeField] GameObject shipPrefab;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        supplyLines = new List<supplyLine>();
        supplyLineGameObjects = new List<ShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int addSupplyLine(supplyLine ssupplyLine, ShipMovement ship)
    {
        int index = supplyLines.Count;
        supplyLines.Add(ssupplyLine);
        supplyLineGameObjects.Add(ship);
        if (!ssupplyLine.planet_b.isDiscovered())
        {
            ssupplyLine.planet_b.addWorker(3);
            ssupplyLine.planet_b.setDiscovered();
            GeneratorController.instance.addNextPlanet();
            ResourceController.instance.updateUnlockedResources();
            UIController.instance.updateAllInfo();
        }
        return index;

    }

    public GameObject getShipPrefab()
    {
        return shipPrefab;
    }

    public void addDelivery(Planet planetA, Planet planetB, Resource resource)
    {
        foreach (supplyLine line in supplyLines)
        {
            if (line.planet_a == planetA && line.planet_b == planetB)
            {
                line.addDelivery(resource, 1, Line.A);
                break;
            }
            if (line.planet_b == planetA && line.planet_a == planetB)
            {
                line.addDelivery(resource, 1, Line.B);
                break;
            }
        }
    }

    public List<supplyLine> getAllSupplyLines()
    {
        return supplyLines;
    }
}
