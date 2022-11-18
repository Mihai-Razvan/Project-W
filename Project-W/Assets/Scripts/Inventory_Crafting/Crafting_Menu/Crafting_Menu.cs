using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_Menu : MonoBehaviour
{
    [SerializeField]
    GameObject[] categoryPanels;
    int openedCategory;
    int selectedItemSlotNumber;         //the index of the selected item in his category in the crafting menu

    public delegate void OnCategoryChanged();
    public static OnCategoryChanged onCategoryChanged;
    public delegate void OnItemSlotChanged();        //when you change an item in the crafting menu
    public static OnItemSlotChanged onitemSlotChanged;        

    void Start()
    {
        for (int i = 0; i < categoryPanels.Length; i++)
            categoryPanels[i].SetActive(false);

        openCategory(0);
        setSelectedItemSlotNumber(0);
        openInitialPanel();
    }


    void Update()
    {
        
    }

    public void openCategory(int categoryNumber)
    {
        categoryPanels[openedCategory].SetActive(false);      //close the currently opened category
        categoryPanels[categoryNumber].SetActive(true);
        openedCategory = categoryNumber;

      //  selectedItemSlotNumber = categoryPanels[openedCategory].transform.GetChild(1).gameObject.GetComponent<Crafting_Item_Slot>().getItemSlotNumber();
        onitemSlotChanged();
      //  openInitialPanel();
        onCategoryChanged();
    }

    public void setSelectedItemSlotNumber(int selectedItemSlotNumber)
    {
        this.selectedItemSlotNumber = selectedItemSlotNumber;
        onitemSlotChanged();
    }

    public void openInitialPanel()       //sets the panel when you change the category
    {
        GameObject firstItemInMenu = categoryPanels[openedCategory].transform.GetChild(1).gameObject;
        int itemCode = firstItemInMenu.GetComponent<Crafting_Item_Slot>().getItemCode();
        int[] resourcesItemCodes = firstItemInMenu.GetComponent<Crafting_Item_Slot>().getResourcesItemCodes();
        int[] resourcesQuantity = firstItemInMenu.GetComponent<Crafting_Item_Slot>().getResourcesQuantities();
        FindObjectOfType<Craft_Panel>().setPanel(itemCode, resourcesItemCodes, resourcesQuantity);
    }

    public int getOpenedCategory()
    {
        return openedCategory;
    }

    public int getSelectedItemSlotNumber()
    {
        return selectedItemSlotNumber;
    }
}
