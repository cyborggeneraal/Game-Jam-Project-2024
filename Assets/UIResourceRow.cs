using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResourceRow : MonoBehaviour
{
    [SerializeField] TMP_Text stockText;
    [SerializeField] TMP_Text surplusText;
    [SerializeField] TMP_Text needsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStockInfo(int stock)
    {
        stockText.text = stock.ToString();
    }

    public void updateSurplusInfo(int surplus, int workers)
    {
        surplusText.text = "+" + surplus.ToString() + "  (" + workers.ToString() + ")";
    }

    public void updateNeedsInfo(int needs)
    {
        needsText.text = "-" + needs.ToString();
    }
}
