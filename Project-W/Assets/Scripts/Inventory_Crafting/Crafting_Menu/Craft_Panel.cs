using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Craft_Panel : MonoBehaviour      //this script is not attached to the craft panel, but to the craft menu
{
    [SerializeField]
    GameObject craftPanel;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    TextMeshProUGUI itemNameText;
    [SerializeField]
    TextMeshProUGUI itemDescriptionText;
    [SerializeField]
    GameObject[] resourcesSlots;
    [SerializeField]
    Button craftButton;

    int itemCode;              //we put this 2 as class members so we can use them in craftButtonClick method
    int[] resourcesItemCodes;
    int[] resourcesQuantity;

    void Start()
    {
        craftButton.onClick.AddListener(craftButtonClick);
    }

    public void setActive(bool active)
    {
        craftPanel.SetActive(active);

        if (active == true)       //to update the craft panel when opening it
            setPanel(itemCode, resourcesItemCodes, resourcesQuantity);
    }

    public void setPanel(int itemCode, int[] resourcesItemCodes, int[] resourcesQuantity)
    {
        this.itemCode = itemCode;
        this.resourcesItemCodes = resourcesItemCodes;
        this.resourcesQuantity = resourcesQuantity;

        int resourcesSlotsNumber = resourcesSlots.Length;
        int resourcesNumber = resourcesItemCodes.Length;

        for (int i = resourcesSlotsNumber - 1; i >= resourcesNumber; i--)     //deactivate the ones we don't need
            resourcesSlots[i].SetActive(false);

        for (int i = 0; i <= resourcesNumber - 1; i++)         //activate all of them
        {
            resourcesSlots[i].SetActive(true);
            Sprite resourceSprite = ItemsList.getSprite(resourcesItemCodes[i]);
            resourcesSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = resourceSprite;

            int availableQuantity = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getTotalQuantity(resourcesItemCodes[i]);
            resourcesSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = availableQuantity + "/" + resourcesQuantity[i];
        }

        Sprite itemSprite = ItemsList.getSprite(itemCode);
        string itemName = ItemsList.getName(itemCode);
        string itemdescription = ItemsList.getDescription(itemCode);

        itemImage.sprite = itemSprite;
        itemNameText.text = itemName;
        itemDescriptionText.text = itemdescription;

        FindObjectOfType<Craft_Button>().changeColors();
    }

    void craftButtonClick()       //we handle the click here, instead of on craft_button class
    {
        if (allResources() == false)
            return;

        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCode, 1, 100);  //we also add a charge in case the item is chargeable

        for (int i = 0; i < resourcesItemCodes.Length; i++)   //consume resources
        {
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().consumeItem(resourcesItemCodes[i], resourcesQuantity[i]);
            int availableQuantity = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getTotalQuantity(resourcesItemCodes[i]);
            resourcesSlots[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = availableQuantity + "/" + resourcesQuantity[i];
        }
    }
    public bool allResources()
    {
        for (int i = 0; i < resourcesItemCodes.Length; i++)
        {
            int availableQuantity = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getTotalQuantity(resourcesItemCodes[i]);
            if (availableQuantity < resourcesQuantity[i])
                return false;
        }

        return true;
    }
}
