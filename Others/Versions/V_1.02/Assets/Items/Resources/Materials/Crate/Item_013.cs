using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_013 : MonoBehaviour
{
    //script used to generate random resources in falling boxes

    [SerializeField]
    int[] possibleItemCodes;      //the itemCodes for the item that could be found in the falling boxes
    [SerializeField]
    int[] chance;

    void Start()
    {
        generateResources();
    }

    void generateResources()
    {
        int resourcesNumber = Random.Range(4, 7);
        int[] itemCodeArray = new int[resourcesNumber];
        int[] quantiyArray = new int[resourcesNumber];
        float[] chargeArray = new float[resourcesNumber];

        for (int i = 0; i < resourcesNumber; i++)
        {
            int randVal = Random.Range(0, 101);
            int count = 0;
        
            for (int j = 0; j < chance.Length; j++)
            {
                count += chance[j];
                if (randVal <= count)
                {
                    itemCodeArray[i] = possibleItemCodes[j];
                    break;
                }
            }
        }

        for (int i = 0; i < resourcesNumber; i++)
        {
            chargeArray[i] = 0;
            quantiyArray[i] = 1;
        }

        this.gameObject.GetComponent<ResourcesData>().setResourceData(itemCodeArray, quantiyArray, chargeArray);
    }
}
