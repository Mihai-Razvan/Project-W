using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crafting_Item_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int itemCode;                   //the itemCode for the itemCode to craft
    [SerializeField]
    int[] resourcesItemCodes;      //resources required to craft this item
    [SerializeField]
    int[] resourcesQuantity;       //quantity required for each resource

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<Craft_Panel>().setActive(true);
        FindObjectOfType<Craft_Panel>().setPanel(itemCode, resourcesItemCodes, resourcesQuantity);
    }
}
