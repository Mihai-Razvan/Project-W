using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Crafting_Panel : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryTab;
    [SerializeField]
    GameObject craftingMenu;
    string status;
    void Start()
    {
        status = "CLOSED";
        inventoryTab.SetActive(false);
        craftingMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (status.Equals("CLOSED"))
            {
                if (FindObjectOfType<Player>().getActionLock().Equals("UNLOCKED"))
                {
                    inventoryTab.SetActive(true);
                    craftingMenu.SetActive(true);
                    status = "OPENED";
                    FindObjectOfType<Player>().setActionLock("INVENTORY_OPENED");

                    FindObjectOfType<Crafting_Menu>().openCategory(0);
                    FindObjectOfType<Craft_Panel>().setActive(false);
                }
            }
            else
            {
                inventoryTab.SetActive(false);
                craftingMenu.SetActive(false);
                status = "CLOSED";
                FindObjectOfType<Player>().setActionLock("UNLOCKED");
            }
        }
    }
}
