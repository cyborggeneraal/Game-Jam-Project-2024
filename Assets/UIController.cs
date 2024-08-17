using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<UIResourceRow> UIRows;
    [SerializeField] List<Resource> UIResources;

    Camera cam;
    [SerializeField] LayerMask planets;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planets))
            {
                DeselectAllPlanets();
                hit.collider.gameObject.GetComponent<Outline>().enabled = true;

                int index = hit.collider.gameObject.GetComponent<PlanetGameObject>().getIndex();
                Planet planet = PlanetsController.instance.getPlanetById(index);

                for (int i = 0; i < UIRows.Count; i++)
                {
                    UIResourceRow UIRow = UIRows[i];
                    Resource resource = UIResources[i];
                    UIRow.updateStockInfo(planet.getStock(resource));
                    UIRow.updateSurplusInfo(planet.getSurplus(resource), planet.getWorkers(resource));
                    UIRow.updateNeedsInfo(planet.getNeeds(resource));
                }

                panel.SetActive(true);
            }
            else
            {
                DeselectAllPlanets();
                panel.SetActive(false);
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
}
