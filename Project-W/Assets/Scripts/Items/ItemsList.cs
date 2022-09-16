using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField]
    Sprite[] spriteArray;
    [SerializeField]
    string[] nameArray;
    [SerializeField]
    int[] inventoryLimit;      //max number per inventory slot for this item (ex 20 resources, 1 grappler)

    public Sprite getSprite(int itemCode)
    {
        return spriteArray[itemCode];
    }

    public string getName(int itemCode)
    {
        return nameArray[itemCode];
    }

    public int getInventoryLimit(int itemCode)
    {
        return inventoryLimit[itemCode];
    }
}
