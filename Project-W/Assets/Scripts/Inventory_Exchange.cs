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
    GameObject dragSlotObject;             //the slot where the drag begins 
    int dragSlotNumber;
    int targetItemCode;       //the code of the item in the target slot
    int targetQuantity;
    GameObject targetSlotObject;
    int targetSlotNumber;
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

    public void dragStart(int itemCode, int quantity, GameObject slot)
    {
        dragItemCode = itemCode;
        dragQuantity = quantity;
        dragSlotObject = slot;
        dragSlotNumber = dragSlotObject.GetComponent<Inventory_Slot>().getSlotNumber();
        state = "ACTIVE";

        if (dragItemCode == 0)     //in case drag begins on an empty slot; we can't do this at the beginning of the method because dragEnd will still be detected so we need the above info
            return;

        itemImage.enabled = true;
        itemImage.sprite = FindObjectOfType<ItemsList>().getSprite(dragItemCode);
    }

    public void dragEnd(int itemCode, int quantity, GameObject slot)
    {
        if (slot == null)     //in case we end the drag over UI (so count in Item_Drop script != 0) but it isn't over a slot
        {
            state = "INACTIVE";
            itemImage.enabled = false;
            return;
        }

        targetItemCode = itemCode;
        targetQuantity = quantity;
        targetSlotObject = slot;
        targetSlotNumber = targetSlotObject.GetComponent<Inventory_Slot>().getSlotNumber();

        if (targetSlotNumber == dragSlotNumber)
        {
            state = "INACTIVE";
            itemImage.enabled = false;
            return;
        }

        if (targetItemCode != dragItemCode)
        {
            if (dragQuantity == dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(dragSlotNumber))   
            {
                targetSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(targetSlotNumber, dragItemCode, dragQuantity);
                dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(dragSlotNumber, targetItemCode, targetQuantity);
            }
            else      // it is a rightclick drag
            {
                if(targetItemCode == 0)    //target is empty
                {
                    targetSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(targetSlotNumber, dragItemCode, dragQuantity);
                    int remainingQuantity = dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(dragSlotNumber) - dragQuantity;
                    dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(dragSlotNumber, dragItemCode, remainingQuantity);
                }
                //else nothing happens
            }
        }
        else
        {
            if(targetQuantity + dragQuantity <= FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode))
            {
                targetSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(targetSlotNumber, dragItemCode, targetQuantity + dragQuantity);
                int remainingQuantity = dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(dragSlotNumber) - dragQuantity;
                dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(dragSlotNumber, dragItemCode, remainingQuantity);
                        
            }
            else
            {
                dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(dragSlotNumber, targetItemCode, dragQuantity + targetQuantity - FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode));
                targetSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(targetSlotNumber, dragItemCode, FindObjectOfType<ItemsList>().getInventoryLimit(targetItemCode));
            }
        }

        dragSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().executeOnInventoryChange();
        targetSlotObject.GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().executeOnInventoryChange();
        state = "INACTIVE";
        itemImage.enabled = false;
    }

    public string getState()
    {
        return state;
    }

    public void disableItemImage()
    {
        state = "INACTIVE";
        itemImage.enabled = false;
    }

    public GameObject getDragSlotObject()
    {
        return dragSlotObject;
    }
}
