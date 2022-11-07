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

        openedCategory = 0;
    }


    void Update()
    {
        
    }

    public void openCategory(int categoryNumber)
    {
        categoryPanels[openedCategory].SetActive(false);      //close the currently opened category
        categoryPanels[categoryNumber].SetActive(true);
        openedCategory = categoryNumber;

        setSelectedItemSlotNumber(0);
        onCategoryChanged();
    }

    public void setSelectedItemSlotNumber(int selectedItemSlotNumber)
    {
        this.selectedItemSlotNumber = selectedItemSlotNumber;
        onitemSlotChanged();
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
