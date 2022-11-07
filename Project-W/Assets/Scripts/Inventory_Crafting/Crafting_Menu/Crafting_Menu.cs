using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_Menu : MonoBehaviour
{
    [SerializeField]
    GameObject[] categoryPanels;
    int openedCategory;

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
    }
}
