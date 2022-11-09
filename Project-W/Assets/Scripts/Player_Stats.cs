using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
    [SerializeField]
    float saturation;
    [SerializeField]
    float thirst;
    [SerializeField]
    float health;
    [SerializeField]
    Image saturationFill;
    [SerializeField]
    Image thirstFill;
    [SerializeField]
    Image healtFill;

    void Start()
    {
        saturationFill.fillAmount = saturation / 100;
        thirstFill.fillAmount = thirst / 100;
        healtFill.fillAmount = health / 100;
    }

    public void changeSaturation(float saturationChange)
    {
        saturation += saturationChange;
        saturationFill.fillAmount = saturation / 100;
    }
    public void changeThirst(float thirstChange)
    {
        thirst += thirstChange;
        thirstFill.fillAmount = thirst / 100;
    }

    public void changeHealth(float healthChange)
    {
        health += healthChange;
        healtFill.fillAmount = health / 100;
    }
}
