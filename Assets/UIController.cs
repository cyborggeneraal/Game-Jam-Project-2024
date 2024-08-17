using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<UIResourceRow> UIRows;
    [SerializeField] TMP_Text idleWorkersCount;
    [SerializeField] GameObject tooltipPanel;
    [SerializeField] TMP_Text tooltipText;

    [SerializeField] GameObject placeShipMessage;

    public static UIController instance;

    enum ClickMode
    {
        defaultMode,
        shipPlaceMode,
        shipDeliverMode
    }

    ClickMode clickMode = ClickMode.defaultMode;

    Camera cam;
    bool onUI = false;
    int selectedIndex = -1;
    [SerializeField] LayerMask planets;

    bool placeShipMode = false;

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
                clickMode = ClickMode.defaultMode;
            }
        }

        if (tooltipPanel.activeSelf)
        {
            tooltipPanel.transform.position = Input.mousePosition;
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
        if (index != selectedIndex)
        {
            Planet planetB = PlanetsController.instance.getPlanetById(index);
            Planet planetA = PlanetsController.instance.getPlanetById(selectedIndex);
            supplyLine supplyLine = planetA.buySupplyLine(Ship.Wooden, planetB);
            GameObject ship = Instantiate(SupplyLineController.instance.getShipPrefab());
            ship.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
            SupplyLineController.instance.addSupplyLine(supplyLine, ship.GetComponent<ShipMovement>());
            Transform planetATransform = PlanetsController.instance.getPlanetGameObjectById(selectedIndex).gameObject.transform;
            Transform planetBTransform = PlanetsController.instance.getPlanetGameObjectById(index).gameObject.transform;
            ship.GetComponent<ShipMovement>().setPlanets(planetATransform, planetBTransform);
            placeShipMessage.SetActive(false);
            clickMode = ClickMode.defaultMode;
        }
    }

    void clickShipDeliver(GameObject clickObject)
    {
        
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
        Planet planet = PlanetsController.instance.getPlanetById(selectedIndex);
        idleWorkersCount.text = planet.idle_workers.ToString();
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

    public void showTooltip(bool show = true)
    {
        tooltipPanel.SetActive(show);
    }

    public void setTooltipText(string text)
    {
        tooltipText.text = text;
    }

    public void buyShips()
    {
        Planet selectedPlanet = PlanetsController.instance.getPlanetById(selectedIndex);
        if (selectedPlanet.getStock(Resource.Wood) >= 10 && selectedPlanet.getStock(Resource.Coal) >= 10)
        {
            placeShipMessage.SetActive(true);
            clickMode = ClickMode.shipPlaceMode;
        }
        else
        {
            Debug.Log("Cannot Buy");
        }
    }
}
