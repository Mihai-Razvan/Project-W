using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Hint : MonoBehaviour       //this is the script that handles the hints for pressing certain keys for certain actions. ex 'R' Rotate or 'E' Collect Resource
{
    [SerializeField]
    GameObject[] hintCategoriesHelper;

    [SerializeField]
    static GameObject[] hintCategories;

    void Start()
    {
        hintCategories = hintCategoriesHelper;

        for (int i = 0; i < hintCategories.Length; i++)
            hintCategories[i].SetActive(false);
    }

    void Update()
    {
        if (Interactions.getInRangeBuilding() == null)
            clearBuildingInteractionHint();
    }

    public static void setBuildingInteractionHint(string hint)
    {
        hintCategories[0].SetActive(true);
        hintCategories[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = hint;
    }

    public static void clearBuildingInteractionHint()
    {
        hintCategories[0].SetActive(false);
    }
}
