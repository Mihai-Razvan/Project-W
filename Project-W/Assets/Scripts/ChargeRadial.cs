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
      //  Player_Inventory.onItemDeselected += resetCharge;
    }

    public void resetCharge()
    {
        progressBar.fillAmount = 0;
    }

    public void setCharge(float chargeValue, float maxCharge)
    {
        progressBar.fillAmount = chargeValue / maxCharge;
    }
}
