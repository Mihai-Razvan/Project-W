using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Crafting_Category_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int categoryNumber;
    [SerializeField]
    Image outline;

    void Awake()
    {
        outline.enabled = false;
        Crafting_Menu.onCategoryChanged += setOutlineEnable;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<Crafting_Menu>().openCategory(categoryNumber);
    }

    void setOutlineEnable()
    {
          if (FindObjectOfType<Crafting_Menu>().getOpenedCategory() == categoryNumber)
             outline.enabled = true;
          else
              outline.enabled = false;
    }

    void OnDestroy()
    {
        Crafting_Menu.onCategoryChanged -= setOutlineEnable;
    }
}
