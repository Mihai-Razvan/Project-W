using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_013 : MonoBehaviour
{
    //script used to generate random resources in falling boxes

    [SerializeField]
    int[] possibleItemCodes;      //the itemCodes for the item that could be found in the falling boxes

    void Start()
    {
        generateResources();
    }

    void generateResources()
    {
        int resourcesNumber = Random.Range(1, 3);
        int[] itemCodeArray = new int[resourcesNumber];
        int[] quantiyArray = new int[resourcesNumber];
        float[] chargeArray = new float[resourcesNumber];
       
        for(int i = 0; i < resourcesNumber; i++)
        {
            itemCodeArray[i] = possibleItemCodes[Random.Range(1, possibleItemCodes.Length)]; 
            quantiyArray[i] = Random.Range(1, 3);
        }

        this.gameObject.GetComponent<ResourcesData>().setResourceData(itemCodeArray, quantiyArray, chargeArray);
    }
}
