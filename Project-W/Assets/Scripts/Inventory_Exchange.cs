using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Exchange : MonoBehaviour
{
    //class used to move items from one slot of inventory to another, from inventory to chest, from chest to inventory, etc

    string state;             //"ACTIVE" if dragging is happening, "INACTIVE" oTHERWISE
    int dragItemCode;         //the cpde of the item that is dragged
    int dragQuantity;
    int dragSlot;             //the slot where the drag begins 
    int targetItemCode;       //the code of the item in the target slot
    int targetQuantity;
    int targetSlot;
    [SerializeField]
    Image itemImage;          

    void Start()
    {
        state = "INACTIVE";
        itemImage.enabled = false;
    }

    
    void Update()
    {
        if(state.Equals("ACTIVE"))
        {
            itemImage.transform.position = Input.mousePosition;
        }
    }

    public void dragStart(int itemCode, int quantity, int slot)
    {
        dragItemCode = itemCode;
        dragQuantity = quantity;
        dragSlot = slot;

        if (dragItemCode == 0)     //in case drag begins on an emty slot
            return;

      //  FindObjectOfType<Inventory>().setSlot(dragSlot, 0, 0);

        Inventory.onInventoryChange();
        state = "ACTIVE";
        itemImage.enabled = true;
        itemImage.sprite = FindObjectOfType<ItemsList>().getSprite(dragItemCode);
    }

    public void dragEnd(int itemCode, int quantity, int slot)
    {
        targetItemCode = itemCode;
        targetQuantity = quantity;
        targetSlot = slot;

        if(targetSlot == dragSlot)
        {
            state = "INACTIVE";
            itemImage.enabled = false;
            return;
        }

        if (targetItemCode != dragItemCode)
        {
            if (dragQuantity == FindObjectOfType<Inventory>().getQuantity(dragSlot))   
            {
                FindObjectOfType<Inventory>().setSlot(targetSlot, dragItemCode, dragQuantity);
                FindObjectOfType<Inventory>().setSlot(dragSlot, targetItemCode, targetQuantity);
            }
            else      // it is a rightclick drag
            {
                if(targetItemCode == 0)    //target is empty
                {
                    FindObjectOfType<Inventory>().setSlot(targetSlot, dragItemCode, dragQuantity);    
                    FindObjectOfType<Inventory>().setSlot(dragSlot, dragItemCode, FindObjectOfType<Inventory>().getQuantity(dragSlot) - dragQuantity);
                }
                //else nothing happens
            }
        }
        else
        {
            if(targetQuantity + dragQuantity <= FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode))
            {
                FindObjectOfType<Inventory>().setSlot(targetSlot, dragItemCode, targetQuantity + dragQuantity);
                if(FindObjectOfType<Inventory>().getQuantity(dragSlot) - dragQuantity == 0)     //this is for left drag
                    FindObjectOfType<Inventory>().setSlot(dragSlot, 0, FindObjectOfType<Inventory>().getQuantity(dragSlot) - dragQuantity);
                else
                    FindObjectOfType<Inventory>().setSlot(dragSlot, dragItemCode, FindObjectOfType<Inventory>().getQuantity(dragSlot) - dragQuantity);
            }
            else
            {
                FindObjectOfType<Inventory>().setSlot(dragSlot, targetItemCode, dragQuantity + targetQuantity - FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode));
                FindObjectOfType<Inventory>().setSlot(targetSlot, dragItemCode, FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode));
            }
        }

        Inventory.onInventoryChange();
        state = "INACTIVE";
        itemImage.enabled = false;
    }
}
