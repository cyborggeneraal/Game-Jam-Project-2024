using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineParent : MonoBehaviour
{
    [SerializeField] Outline[] children;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOutline(bool set)
    {
        foreach (Outline child in children)
        {
            child.enabled = set;
        }
    }
}
