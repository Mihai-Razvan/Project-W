using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory_Slot : MonoBehaviour, IPointerDownHandler, IDropHandler, IDragHandler, IPointerClickHandler
{
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI quantityText;
    [SerializeField]
    private int slot;

    void Start()
    {
        itemImage.gameObject.SetActive(false);
        quantityText.gameObject.SetActive(false);
        Inventory.onInventoryChange += onChange;
    }

    void onChange()
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);

        if (itemCode != 0)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = FindObjectOfType<ItemsList>().getSprite(itemCode);
            quantityText.gameObject.SetActive(true);
            quantityText.text = FindObjectOfType<Inventory>().getQuantity(slot).ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);
        int quantity = 0;

        if (eventData.button == PointerEventData.InputButton.Left)
            quantity = FindObjectOfType<Inventory>().getQuantity(slot);
        else if (eventData.button == PointerEventData.InputButton.Right)
            quantity = 1;

        FindObjectOfType<Inventory_Exchange>().dragStart(itemCode, quantity, slot);
    }

    public void OnDrop(PointerEventData eventData)
    {
        int itemCode = FindObjectOfType<Inventory>().getItemCode(slot);
        int quantity = FindObjectOfType<Inventory>().getQuantity(slot);

        FindObjectOfType<Inventory_Exchange>().dragEnd(itemCode, quantity, slot);
    }

    public void OnPointerClick(PointerEventData eventData)     //in case a slot is just clicked but without any darg
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
