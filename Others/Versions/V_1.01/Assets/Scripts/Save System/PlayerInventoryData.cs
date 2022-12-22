using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventoryData 
{
    public int[] itemCodeArray;
    public int[] quantityArray;
    public float[] chargeArray;

    public PlayerInventoryData(ArrayList data)
    {
        this.itemCodeArray = (int[])data[0];
        this.quantityArray = (int[])data[1];
        this.chargeArray = (float[])data[2];
    }
}
