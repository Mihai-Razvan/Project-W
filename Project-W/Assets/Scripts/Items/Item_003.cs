using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_003 : MonoBehaviour
{
    int itemCode;
    int quantity;

    public void setBox(int itemCode, int quantity)
    {
        this.itemCode = itemCode;
        this.quantity = quantity;
    }

    public int getItemCode()
    {
        return itemCode;
    }

    public int getQuantity()
    {
        return quantity;
    }
}
