using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Crafting_Item_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int itemSlotNumber;       //the index in his category
    [SerializeField]
    Image outline;
    [SerializeField]
    int itemCode;                   //the itemCode for the itemCode to craft
    [SerializeField]
    int[] resourcesItemCodes;      //resources required to craft this item
    [SerializeField]
    int[] resourcesQuantity;       //quantity required for each resource

    void Awake()
    {
        outline.enabled = false;
        Crafting_Menu.onitemSlotChanged += setOutlineEnable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<Craft_Panel>().setActive(true);
        FindObjectOfType<Craft_Panel>().setPanel(itemCode, resourcesItemCodes, resourcesQuantity);
        FindObjectOfType<Crafting_Menu>().setSelectedItemSlotNumber(itemSlotNumber);
    }

    void setOutlineEnable()
    {
        if (FindObjectOfType<Crafting_Menu>().getSelectedItemSlotNumber() == itemSlotNumber)
            outline.enabled = true;
        else
            outline.enabled = false;
    }

    public int getItemSlotNumber()
    {
        return itemSlotNumber;
    }

    public int getItemCode()
    {
        return itemCode;
    }

    public int[] getResourcesItemCodes()
    {
        return resourcesItemCodes;
    }

    public int[] getResourcesQuantities()
    {
        return resourcesQuantity;
    }

    void OnDestroy()
    {
        Crafting_Menu.onitemSlotChanged -= setOutlineEnable;
    }
}
