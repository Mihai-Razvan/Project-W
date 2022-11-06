using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Inventory : MonoBehaviour
{                                        //the firt 10 slots in the inventory are for the inventory bar
    [SerializeField]
    Image infoSlotImage;     //the top image that shows the hovered item
    [SerializeField]
    TextMeshProUGUI hoveredItemName;
    [SerializeField]
    TextMeshProUGUI hoveredItemDescription;
    int selectedInventorySlot;
    int selectedItemCode;
    KeyCode[] keyCodes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    public delegate void OnItemSelected();       //when a new inventory slot from the inventory bar is selected
    public static OnItemSelected onItemSelected;
    public delegate void OnItemDeselected();
    public static OnItemDeselected onItemDeselected;



    private void Awake()
    {
        infoSlotImage.enabled = false;
        hoveredItemName.enabled = false;
        hoveredItemDescription.enabled = false;
        selectedInventorySlot = 0;
        selectedItemCode = 0;
        onItemDeselected += Item.destroyUsedObject;
    }

    void Update()
    {
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
            selectedItemCode = getPlayerInventoryHolder().GetComponent<Inventory>().getItemCode(selectedInventorySlot);
            onItemDeselected();
            onItemSelected();
        }
    }

    public void setHoveredItem(int itemCode)   //used to show the sprite, name and description for the hovered item
    {
        if (itemCode != 0)
        {
            infoSlotImage.enabled = true;
            hoveredItemName.enabled = true;
            hoveredItemDescription.enabled = true;
            infoSlotImage.sprite = FindObjectOfType<ItemsList>().getSprite(itemCode);
            hoveredItemName.text = FindObjectOfType<ItemsList>().getName(itemCode);
            hoveredItemDescription.text = FindObjectOfType<ItemsList>().getDescription(itemCode); 
        }
        else
        {
            infoSlotImage.enabled = false;
            hoveredItemName.enabled = false;
            hoveredItemDescription.enabled = false;
        }
    }

    public int getSelectedItemCode()  //return the itemCode for the selected item
    {
        return getPlayerInventoryHolder().GetComponent<Inventory>().getItemCode(selectedInventorySlot);
    }

    public int getSelectedSlot() 
    {
        return selectedInventorySlot;
    }

    public GameObject getPlayerInventoryHolder()          //the object that holds the player invenotry script holds also the inventory
    {
        return this.gameObject;
    }

}
