using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{                                        //the firt 10 slots in the inventory are for the inventory bar

    string inventoryTabState;
    [SerializeField]
    GameObject inventoryTab;
    int selectedInventorySlot;
    int selectedItemCode;
    KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    public delegate void OnItemSelected();       //when a new inventory slot from the inventory bar is selected
    public static OnItemSelected onItemSelected;
    public delegate void OnItemDeselected();
    public static OnItemDeselected onItemDeselected;
  //  public delegate void OnInventoryChange();    //when a modification occurs in the inventory
  //  public static OnInventoryChange onInventoryChange;

    private void Awake()
    {
        inventoryTabState = "CLOSED";
        inventoryTab.SetActive(false);
        selectedInventorySlot = 0;
        selectedItemCode = 0;
        onItemSelected += Item.destroyUsedObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryTabState.Equals("CLOSED"))
            {
                if (FindObjectOfType<Player>().getActionLock().Equals("UNLOCKED"))
                {
                    inventoryTab.SetActive(true);
                    inventoryTabState = "OPENED";
                    FindObjectOfType<Player>().setActionLock("INVENTORY_OPENED");
                }
            }
            else
            {
                inventoryTab.SetActive(false);
                inventoryTabState = "CLOSED";
                FindObjectOfType<Player>().setActionLock("UNLOCKED");
            }
        }

        selectInventorySlot();
    }

    void selectInventorySlot()     //when you press a numeric key on keyboard it will select that slot in inventory bar
    {
        if (FindObjectOfType<Player>().getActionLock().Equals("ACTION_LOCKED"))
            return;

        bool newSelection = false;

        for (int i = 1; i <= 9; i++)
            if (Input.GetKeyDown(keyCodes[i - 1]))
            {
                selectedInventorySlot = i - 1;
                newSelection = true;
                break;
            }

        if (newSelection || getPlayerInventoryHolder().GetComponent<Inventory>().getItemCode(selectedInventorySlot) != selectedItemCode)
        {
            onItemSelected();
            onItemDeselected();
            selectedItemCode = getPlayerInventoryHolder().GetComponent<Inventory>().getItemCode(selectedInventorySlot);
        }
    }

    public int getSelectedItem()  //return the itemCode for the selected item
    {
        return getPlayerInventoryHolder().GetComponent<Inventory>().getItemCode(selectedInventorySlot);
    }
    
    public GameObject getPlayerInventoryHolder()          //the object that holds the player invenotry script holds also the inventory
    {
        return this.gameObject;
    }

}
