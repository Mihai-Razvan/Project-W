using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    int[] quantityArray;

    public void addItem(string itemTag, int quantity)
    {
        int itemCode = Item.getItemCode(itemTag);

        for(int i = 0; i < itemCodeArray.Length; i++)
            if(itemCodeArray[i] == itemCode || itemCodeArray[i] == 0)
            {
                itemCodeArray[i] = itemCode;
                quantityArray[i] += quantity;
                break;
            }
    }
}
