using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasScpt : MonoBehaviour
{
    public static GameObject canvasObject;
    // Start is called before the first frame update
    void Awake()
    {
        canvasObject = this.gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
