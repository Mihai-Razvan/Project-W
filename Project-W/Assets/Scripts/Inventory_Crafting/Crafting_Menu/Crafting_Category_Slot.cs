using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crafting_Category_Slot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int categoryNumber;

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<Crafting_Menu>().openCategory(categoryNumber);
    }


}
