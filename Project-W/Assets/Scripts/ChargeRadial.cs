using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeRadial : MonoBehaviour
{
    static Image progressBar;
    [SerializeField]
    Image progressBarHelper;

    void Start()
    {
        //  Player_Inventory.onItemDeselected += resetCharge;
        progressBar = progressBarHelper;
    }

    public static void resetCharge()
    {
        progressBar.fillAmount = 0;
    }

    public void setCharge(float chargeValue, float maxCharge)
    {
        progressBar.fillAmount = chargeValue / maxCharge;
    }
}
