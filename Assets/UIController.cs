using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMesh woodCount;

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
                int index = hit.collider.gameObject.GetComponent<PlanetGameObject>().getIndex();
                Planet planet = PlanetsController.instance.getPlanetById(index);
                woodCount.text = planet.stock[Resource.Wood].ToString();
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }
}
