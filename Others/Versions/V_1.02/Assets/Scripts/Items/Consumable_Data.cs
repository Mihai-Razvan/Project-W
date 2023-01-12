using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable_Data : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    int[] saturationIncrease;
    [SerializeField]
    int[] thirstIncrease;

    public int checkConsumableIndex(int itemCode)      //checks if a given item is consumable
    {
        for (int i = 0; i < itemCodeArray.Length; i++)
            if (itemCodeArray[i] == itemCode)
                return i;

        return -1;
    }

    public int getSaturationIncrease(int itemCode)
    {
        if (checkConsumableIndex(itemCode) != -1)
            return saturationIncrease[checkConsumableIndex(itemCode)];

        return 0;
    }

    public int getThirstIncrease(int itemCode)
    {
        if (checkConsumableIndex(itemCode) != -1)
            return thirstIncrease[checkConsumableIndex(itemCode)];

        return 0;
    }
}
