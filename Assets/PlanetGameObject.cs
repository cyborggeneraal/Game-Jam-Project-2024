using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGameObject : MonoBehaviour
{
    [SerializeField] Planet planet;
    [SerializeField] GameObject[] planetVariants;
    [SerializeField] GameObject fog;

    int index;

    // Start is called before the first frame update
    void Awake()
    {
        Planet planet = new Planet(0, 0, 0);
        index = PlanetsController.instance.addPlanet(planet, this);
        setVariant(Random.Range(0, planetVariants.Length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getIndex()
    {
        return index;
    }

    public void setVariant(int variant)
    {
        foreach (GameObject planetVariant in planetVariants)
        {
            planetVariant.SetActive(false);
        }
        planetVariants[variant].SetActive(true);
    }

    public void turnOffFog()
    {
        fog.SetActive(false);
    }
}
