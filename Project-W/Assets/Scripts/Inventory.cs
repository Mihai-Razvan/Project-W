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
        int inventoryLimit = FindObjectOfType<ItemsList>().getInventoryLimit(itemCode);

        for (int i = 0; i < itemCodeArray.Length; i++)
            if (itemCodeArray[i] == itemCode)
            {
                if (quantityArray[i] + quantity < inventoryLimit)
                {
                    quantityArray[i] += quantity;
                    quantity = 0;
                }
                else
                {
                    quantity -= inventoryLimit - quantityArray[i];
                    quantityArray[i] = inventoryLimit;
                }

                if (quantity == 0)
                    break;
            }

        if (quantity != 0)         //after filling all slots that already got this item, if we still have remaining quantity we search for a free slot to add
            for (int i = 0; i < itemCodeArray.Length; i++)
                if (itemCodeArray[i] == 0)
                {
                    if (quantity <= inventoryLimit)
                    {
                        itemCodeArray[i] = itemCode;
                        quantityArray[i] = quantity;
                        quantity = 0;
                    }
                    else
                    {
                        itemCodeArray[i] = itemCode;
                        quantity -= inventoryLimit;
                        quantityArray[i] = inventoryLimit;
                    }

                    if (quantity == 0)
                        break;
                }

        if (quantity != 0)                 //it isn't enought space in the inventory for all the quantity
            Debug.Log("Not enought space: " + quantity + " remained!");
    }

    public int getNumberOfSlots()
    {
        return itemCodeArray.Length;
    }

    public int getItemCode(int slot)    //return the item code for the given inventory slot
    {
        return itemCodeArray[slot];
    }

    public int getQuantity(int slot)
    {
        return quantityArray[slot];
    }

    public void setSlot(int slot, int itemCode, int quantity)      //sets the code and quantity for the given slot
    {
        itemCodeArray[slot] = itemCode;
        quantityArray[slot] = quantity;
    }

}
