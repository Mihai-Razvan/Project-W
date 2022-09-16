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

        FindObjectOfType<Inventory>().setSlot(dragSlot, 0, 0);

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

        FindObjectOfType<Inventory>().setSlot(targetSlot, dragItemCode, dragQuantity);
        FindObjectOfType<Inventory>().setSlot(dragSlot, targetItemCode, targetQuantity);

        Inventory.onInventoryChange();
        state = "INACTIVE";
        itemImage.enabled = false;
    }
}
