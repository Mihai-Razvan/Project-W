using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeRadial : MonoBehaviour
{
    [SerializeField]
    Image progressBar;

    void Start()
    {
        Player_Inventory.onItemDeselected += resetBar;
    }

    void Update()
    {
        if (FindObjectOfType<Item_002>().getLaserState().Equals("UNUSED"))     
            progressBar.fillAmount = FindObjectOfType<Item_002>().getChargeTime() / FindObjectOfType<Item_002>().getMaxChargeTime();
        else
            progressBar.fillAmount = 0;
    }

    void resetBar()
    {
        progressBar.fillAmount = 0;
    }
}
