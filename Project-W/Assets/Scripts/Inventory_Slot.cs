using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_Slot : MonoBehaviour, IPointerDownHandler
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

    void Update()
    {
        
    }

    void changeSprite()
    {
        int itemCode = FindObjectOfType<Inventory>().getItem(slot);

        if (itemCode != 0)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = FindObjectOfType<ItemsList>().getSprite(itemCode);
        }
        else
            itemImage.gameObject.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        
    }
}
