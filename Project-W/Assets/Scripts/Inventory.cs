using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    int[] quantityArray;

    int selectedInventorySlot;
    KeyCode[] keyCodes = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9};

    private void Start()
    {
        selectedInventorySlot = 0;
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

        if(newSelection)
            Item.destroyUsedObject();
    }

    public int getSelectedItem()  //return the itemCode for the selected item
    {
        return itemCodeArray[selectedInventorySlot];
    }
}
