using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResourceRow : MonoBehaviour
{
    [SerializeField] TMP_Text stockText;
    [SerializeField] TMP_Text surplusText;
    [SerializeField] TMP_Text needsText;
    [SerializeField] Button plusWorkerButton;
    [SerializeField] Button minWorkerButton;

    [SerializeField] Resource resource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateInfo()
    {
        Planet selectedPlanet = PlanetsController.instance.getPlanetById(UIController.instance.getSelectedIndex());
        updateStockInfo(selectedPlanet);
        updateSurplusInfo(selectedPlanet);
        updateNeedsInfo(selectedPlanet);
    }

    public void updateStockInfo(Planet planet)
    {
        stockText.text = planet.getStock(resource).ToString();
    }

    public void updateSurplusInfo(Planet planet)
    {
        surplusText.text = "+" + planet.getSurplus(resource).ToString() + "  (" + planet.getWorkers(resource).ToString() + ")";
    }

    public void updateNeedsInfo(Planet planet)
    {
        needsText.text = "-" + planet.getNeeds(resource).ToString();
    }

    public Button getPlusButton()
    {
        return plusWorkerButton;
    }

    public Button getMinButton()
    {
        return minWorkerButton;
    }

    public void pressPlusButton()
    {
        PlanetsController.instance.getPlanetById(UIController.instance.getSelectedIndex()).assignWorker(resource);
        updateInfo();
        UIController.instance.updateIdleWorkers();
    }

    public void pressMinButton()
    {
        PlanetsController.instance.getPlanetById(UIController.instance.getSelectedIndex()).unassignWorker(resource);
        updateInfo();
        UIController.instance.updateIdleWorkers();
    }
}
