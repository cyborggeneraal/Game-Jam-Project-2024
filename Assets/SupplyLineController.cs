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
        return index;

    }

    public GameObject getShipPrefab()
    {
        return shipPrefab;
    }
}
