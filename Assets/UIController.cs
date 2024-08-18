using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] List<UIResourceRow> UIRows;
    [SerializeField] TMP_Text idleWorkersCount;

    [SerializeField] GameObject placeShipMessage;
    public GameObject deliverShipMessage;

    public static UIController instance;

    public enum ClickMode
    {
        defaultMode,
        shipPlaceMode,
        shipDeliverMode
    }

    public ClickMode clickMode = ClickMode.defaultMode;

    Camera cam;
    public bool onUI = false;
    public int selectedIndex = -1;
    public Resource selectedResource = Resource.Wood;
    [SerializeField] LayerMask planets;

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
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !onUI)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planets))
            {
                GameObject clickObject = hit.collider.gameObject;
                switch (clickMode)
                {
                    case ClickMode.defaultMode:
                        clickDefault(clickObject);
                        break;
                    case ClickMode.shipPlaceMode:
                        clickShipPlace(clickObject);
                        break;
                    case ClickMode.shipDeliverMode:
                        clickShipDeliver(clickObject);
                        break;
                }
            }
            else
            {
                DeselectAllPlanets();
                panel.SetActive(false);
                selectedIndex = -1;
                placeShipMessage.SetActive(false);
                deliverShipMessage.SetActive(false);
                clickMode = ClickMode.defaultMode;
            }
        }

    }

    void clickDefault(GameObject clickObject)
    {
        DeselectAllPlanets();
        clickObject.GetComponent<OutlineParent>().setOutline(true);

        int index = clickObject.GetComponent<PlanetGameObject>().getIndex();

        selectedIndex = index;

        updateAllInfo();
        updateIdleWorkers();

        panel.SetActive(true);
    }

    void clickShipPlace(GameObject clickObject)
    {
        updateAllInfo();
        int index = clickObject.GetComponent<PlanetGameObject>().getIndex();
        int selectedIndex = UIController.instance.selectedIndex;
        bool check = false;
        Planet planetB = PlanetsController.instance.getPlanetById(index);
        Planet planetA = PlanetsController.instance.getPlanetById(selectedIndex);
        foreach (supplyLine line in SupplyLineController.instance.getAllSupplyLines())
        {
            if (line.planet_a == planetA && line.planet_b == planetB ||
                line.planet_a == planetB && line.planet_b == planetA)
            {
                check = true;
                break;
            }
        }
        if (index != selectedIndex && !check)
        {
            supplyLine supplyLine = planetA.buySupplyLine(Ship.Wooden, planetB);
            GameObject ship = Instantiate(SupplyLineController.instance.getShipPrefab());
            ship.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
            SupplyLineController.instance.addSupplyLine(supplyLine, ship.GetComponent<ShipMovement>());
            Transform planetATransform = PlanetsController.instance.getPlanetGameObjectById(selectedIndex).gameObject.transform;
            Transform planetBTransform = PlanetsController.instance.getPlanetGameObjectById(index).gameObject.transform;
            ship.GetComponent<ShipMovement>().setPlanets(planetATransform, planetBTransform);
            placeShipMessage.SetActive(false);
            clickMode = ClickMode.defaultMode;
            panel.SetActive(true);
            PlanetsController.instance.getPlanetGameObjectById(index).turnonSatisfaction();
        }
    }

    void clickShipDeliver(GameObject clickObject)
    {
        int index = clickObject.GetComponent<PlanetGameObject>().getIndex();
        int selectedIndex = UIController.instance.selectedIndex;
        if (index != selectedIndex)
        {
            Planet planetB = PlanetsController.instance.getPlanetById(index);
            Planet planetA = PlanetsController.instance.getPlanetById(selectedIndex);

            SupplyLineController.instance.addDelivery(planetA, planetB, selectedResource);

            clickMode = ClickMode.defaultMode;
            panel.SetActive(true);
            UIController.instance.deliverShipMessage.SetActive(false);

            updateAllInfo();
        }
    }

    void DeselectAllPlanets()
    {
        foreach (PlanetGameObject planetGameObject in PlanetsController.instance.getAllPlanetGameObjects())
        {
            planetGameObject.gameObject.GetComponent<OutlineParent>().setOutline(false);
        }
    }

    public void setOnUI(bool onUI)
    {
        this.onUI = onUI;
    }

    public int getSelectedIndex()
    {
        return selectedIndex;
    }

    public void updateIdleWorkers()
    {
        if (selectedIndex != -1)
        {
            Planet planet = PlanetsController.instance.getPlanetById(selectedIndex);
            idleWorkersCount.text = planet.idle_workers.ToString();
        }
    }

    public void updateAllInfo()
    {
        for (int i = 0; i < UIRows.Count; i++)
        {
            if (i < ResourceController.instance.unlockedResources.Count)
            {
                UIRows[i].gameObject.SetActive(true);
                UIRows[i].resource = ResourceController.instance.unlockedResources[i];
            }
            else
            {
                UIRows[i].gameObject.SetActive(false);
            }
        }
        foreach (UIResourceRow row in UIRows)
        {
            row.updateInfo();
        }
        updateIdleWorkers();
    }

    public void buyShips()
    {
        Planet selectedPlanet = PlanetsController.instance.getPlanetById(selectedIndex);
        if (selectedPlanet.getStock(Resource.Wood) >= 10 && selectedPlanet.getStock(Resource.Coal) >= 10)
        {
            placeShipMessage.SetActive(true);
            panel.SetActive(false);
            onUI = false;
            clickMode = ClickMode.shipPlaceMode;
        }
    }

    public void buyWorkers()
    {
        Planet selectedPlanet = PlanetsController.instance.getPlanetById(selectedIndex);
        if (selectedPlanet.getStock(Resource.Wheat) >= 10)
        {
            selectedPlanet.buyWorker(1);
        }
        updateAllInfo();
    }
}
