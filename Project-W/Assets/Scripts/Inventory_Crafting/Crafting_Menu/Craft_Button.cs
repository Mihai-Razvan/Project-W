using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Craft_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler     //the click is not handled in this class, but in craft_panel!!!!
{
    [SerializeField]
    Sprite craftButtonDefaultSprite;
    [SerializeField]
    Sprite craftButtonGreenSprite;
    [SerializeField]
    Sprite craftButtonRedSprite;

    public void changeColors()
    {
        if (FindObjectOfType<Craft_Panel>().allResources() == true)
            this.gameObject.GetComponent<Image>().sprite = craftButtonGreenSprite;
        else
            this.gameObject.GetComponent<Image>().sprite = craftButtonRedSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FindObjectOfType<Craft_Panel>().allResources() == true)
            this.gameObject.GetComponent<Image>().sprite = craftButtonDefaultSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        changeColors();
    }
}
