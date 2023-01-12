using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeRadial : MonoBehaviour          //this script is on canvs -> crosshair -> charge radial
{
    static Image progressBar;
    static Image centerDot;
    static Color fullChargeColor;
    static Color defaultColor;

    [SerializeField]
    Image progressBarHelper;
    [SerializeField]
    Image centerDotHelper;
    [SerializeField]
    Color fullChargeColorHelper;

    void Start()
    {
        progressBar = progressBarHelper;
        centerDot = centerDotHelper;
        fullChargeColor = fullChargeColorHelper;
        defaultColor = new Color(255, 255, 255);
    }

    public static void resetCharge()
    {
        progressBar.fillAmount = 0;
        progressBar.color = defaultColor;
        centerDot.color = defaultColor;
    }

    public static void setCharge(float chargeValue, float maxCharge, bool maxChargeMatters)
    {
        progressBar.fillAmount = chargeValue / maxCharge;

        if (maxChargeMatters == false)
            return;

        if (chargeValue / maxCharge >= 1)
        {
            progressBar.color = fullChargeColor;
            centerDot.color = fullChargeColor;
        }
        else
        {
            progressBar.color = defaultColor;
            centerDot.color = defaultColor;
        }
    }
}
