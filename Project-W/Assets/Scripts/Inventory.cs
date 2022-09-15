using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;    //the first 10 slots are for the inventory bar
    [SerializeField]
    int[] quantityArray;

    int selectedInventorySlot;
    KeyCode[] keyCodes = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9};

    public delegate void OnItemSelected();       //when a new inventory slot from the inventory bar is selected
    public static OnItemSelected onItemSelected;
    public delegate void OnInventoryChange();    //when a modification occurs in the inventory
    public static OnInventoryChange onInventoryChange;

    private void Awake()
    {
        selectedInventorySlot = 0;
        onItemSelected += Item.destroyUsedObject;
    }

    void Update()
    {
        selectInventorySlot();
    }

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

        onInventoryChange();
    }

    void selectInventorySlot()     //when you press a numeric key on keyboard it will select that slot in inventory bar
    {
        bool newSelection = false;

        for(int i = 1; i <= 9; i++)
            if(Input.GetKeyDown(keyCodes[i - 1]))
            {
                selectedInventorySlot = i - 1;
                newSelection = true;
                break;
            }

        if (newSelection)
            onItemSelected();
    }

    public int getSelectedItem()  //return the itemCode for the selected item
    {
        return itemCodeArray[selectedInventorySlot];
    }

    public int getItem(int slot)    //return the item code for the given inventory slot
    {
        return itemCodeArray[slot];
    }
}
