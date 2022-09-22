using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_003 : Item
{
    int holdedItemCode;
    int holdedQuantity;

    public void setBox(int itemCode, int quantity)
    {
        this.holdedItemCode = itemCode;
        this.holdedQuantity = quantity;
    }

    public int getItemCode()
    {
        return holdedItemCode;
    }

    public int getQuantity()
    {
        return holdedQuantity;
    }
}
