using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : MonoBehaviour
{
    //this script is attached to resources to know their code an quantities; it is required for resources that give you more resources (liek falling box)

    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    int[] quantityArray;

    public void setResourceData(int[] itemCodeArray, int[] quantityArray)
    {
        this.itemCodeArray = itemCodeArray;
        this.quantityArray = quantityArray;
    }

    public int[] getItemCodeArray()
    {
        return itemCodeArray;
    }

    public int[] getQuantityArray()
    {
        return quantityArray;
    }
}
