using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_034 : MonoBehaviour
{
    //script used to generate random number of candies for gift crate
    [SerializeField]
    int candyItemCode;

    void Start()
    {
        generateResources();
    }

    void generateResources()
    {
        int resourcesNumber = Random.Range(1, 5);
        int[] itemCodeArray = new int[1] { candyItemCode };
        int[] quantiyArray = new int[1] { resourcesNumber };
        float[] chargeArray = new float[1] { 0 };

        this.gameObject.GetComponent<ResourcesData>().setResourceData(itemCodeArray, quantiyArray, chargeArray);
    }
}
