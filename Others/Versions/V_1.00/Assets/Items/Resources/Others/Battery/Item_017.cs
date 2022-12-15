using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_017 : MonoBehaviour
{
    float charge;

    void Start()
    {
        charge = 100;
    }

    public float getCharge()
    {
        return charge;
    }

    public void setCharge(float charge)
    {
        this.charge = charge;
    }

    public void increaseCharge(float increaseAmount)
    {
        charge += increaseAmount;
    }

    public void decreaseCharge(float decreaseAmount)
    {
        charge = decreaseAmount;
    }
}
