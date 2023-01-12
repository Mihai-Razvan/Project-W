using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : MonoBehaviour
{
    //this script is attached to resources to know their code an quantities; it is required for resources that give you more resources (like falling box)

    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    int[] quantityArray;
    [SerializeField]
    float[] chargeArray;

    public void setResourceData(int[] itemCodeArray, int[] quantityArray, float[] chargeArray)
    {
        this.itemCodeArray = itemCodeArray;
        this.quantityArray = quantityArray;
        this.chargeArray = chargeArray;
    }

    public int[] getItemCodeArray()
    {
        return itemCodeArray;
    }

    public int[] getQuantityArray()
    {
        return quantityArray;
    }

    public float[] getChargeArray()
    {
        return chargeArray;
    }
}
