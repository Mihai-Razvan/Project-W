using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IPointerDownHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    private int slot;

    void Start()
    {
        itemImage.gameObject.SetActive(false);
        Inventory.onInventoryChange += changeSprite;
    }

    void changeSprite()
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);

        if (itemCode != 0)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = FindObjectOfType<ItemsList>().getSprite(itemCode);
        }
        else
            itemImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);
        int quantity = FindObjectOfType<Inventory>().getQuantity(slot);

        FindObjectOfType<Inventory_Exchange>().dragStart(itemCode, quantity, slot);
    }

    public void OnDrop(PointerEventData eventData)
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);
        int quantity = FindObjectOfType<Inventory>().getQuantity(slot);

        FindObjectOfType<Inventory_Exchange>().dragEnd(itemCode, quantity, slot);
    }

    public void OnDrag(PointerEventData eventData)     //if we don't have this the start and end drag don't work; idk why
    {
        
    }

    public int getSlot()
    {
        return slot;
    }
}
