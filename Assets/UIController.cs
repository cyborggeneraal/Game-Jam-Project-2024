using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<UIResourceRow> UIRows;
    [SerializeField] List<Resource> UIResources;
    [SerializeField] TMP_Text idleWorkersCount;

    public static UIController instance;

    Camera cam;
    bool onUI = false;
    int selectedIndex = -1;
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
                DeselectAllPlanets();
                hit.collider.gameObject.GetComponent<Outline>().enabled = true;

                int index = hit.collider.gameObject.GetComponent<PlanetGameObject>().getIndex();
                Planet planet = PlanetsController.instance.getPlanetById(index);

                selectedIndex = index;

                foreach (UIResourceRow UIRow in UIRows)
                {
                    UIRow.updateInfo();
                }
                updateIdleWorkers();

                panel.SetActive(true);
            }
            else
            {
                DeselectAllPlanets();
                panel.SetActive(false);
                selectedIndex = -1;
            }
        }
    }

    void DeselectAllPlanets()
    {
        foreach (PlanetGameObject planetGameObject in PlanetsController.instance.getAllPlanetGameObjects())
        {
            planetGameObject.gameObject.GetComponent<Outline>().enabled = false;
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
}
