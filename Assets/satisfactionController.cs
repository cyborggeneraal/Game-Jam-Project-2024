using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satisfactionController : MonoBehaviour
{
    [SerializeField] GameObject satisfactionMeter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSatisfaction(int satisfaction)
    {
        Debug.Log(satisfaction);
        satisfactionMeter.transform.localScale = new Vector3(((float) satisfaction)/100.0f, 1.0f, 1.0f);
    }
}
