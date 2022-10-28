using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;    
    [SerializeField]
    int[] quantityArray;
    [SerializeField]
    float[] chargeArray;       
    public delegate void OnInventoryChange();
    public OnInventoryChange onInventoryChange;

    void Awake() //we need awake because if we use start onChange() from Inventory_Slot will be assigned to onInventoryChange vefore checkEmpty
    {
        onInventoryChange += checkEmpty;
    }

    public int addItem(int itemCode, int quantity)
    {
        int inventoryLimit = FindObjectOfType<ItemsList>().getInventoryLimit(itemCode);
        int addedSlot = -1; //this is used for the items that can be only one per slot so when you add it adds maximum to one slot; used for ex for batteries to know the slot and set slot charge

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
                        addedSlot = i;
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

        onInventoryChange();
        return addedSlot;
    }

    void checkEmpty()
    {
        for (int i = 0; i < itemCodeArray.Length; i++)
            if (quantityArray[i] == 0)
            {
                itemCodeArray[i] = 0;
                chargeArray[i] = 0;
            }
    }

    public void decreaseQuantity(int quantity, int slot)    //we know the slot 
    {
        quantityArray[slot] -= quantity;
        onInventoryChange();
    }


    public void executeOnInventoryChange()
    {
        onInventoryChange();
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

    public void setSlot(int slot, int itemCode, int quantity, float charge)      //sets the code and quantity for the given slot
    {
        if (quantity == 0)
            itemCodeArray[slot] = 0;
        else
            itemCodeArray[slot] = itemCode;

        quantityArray[slot] = quantity;
        chargeArray[slot] = charge;

        onInventoryChange();
    }

    public void setCharge(int slot, float charge)
    {
        chargeArray[slot] = charge;
        onInventoryChange();
    }

    public void decreaseCharge(int slot, int decreaseAmount)
    {
        chargeArray[slot] -= decreaseAmount;
        onInventoryChange();
    }

    public float getCharge(int slot)
    {
        return chargeArray[slot];
    }
}
