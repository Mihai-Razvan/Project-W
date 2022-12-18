using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Crafting_Panel : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryTab;
    [SerializeField]
    GameObject craftingMenu;
    static string status;

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
                if (ActionLock.getActionLock().Equals("UNLOCKED"))
                {
                    open();

                    ActionLock.setActionLock("UI_OPENED");
                    UnityEngine.Cursor.visible = true;
                }
            }
            else
            {
                close();
                ActionLock.setActionLock("UNLOCKED");
                UnityEngine.Cursor.visible = false;
            }
        }
    }

    public void open()
    {
        inventoryTab.SetActive(true);
        craftingMenu.SetActive(true);
        FindObjectOfType<Craft_Panel>().setActive(true);
        status = "OPENED";
    }

    public void close()
    {
        inventoryTab.SetActive(false);
        craftingMenu.SetActive(false);
        status = "CLOSED";
    }

    public static string getInventoryStatus()
    {
        return status;
    }
}
