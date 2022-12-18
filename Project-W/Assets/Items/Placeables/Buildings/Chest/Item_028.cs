using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_028 : Item       //chest
{
    string status;            //OPENED or CLOSED
    [SerializeField]
    GameObject chestCanvas;
    [SerializeField]
    Animator animator;


    void Awake()  //we use awake because if we use start it is called before load from the save file
    {
        status = "CLOSED";
        chestCanvas.SetActive(false);
    }


    void Update()
    {
        if (Interactions.getInRangeBuilding() == this.gameObject && (ActionLock.getActionLock().Equals("UNLOCKED") || (ActionLock.getActionLock().Equals("UI_OPENED") && status.Equals("OPENED"))))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }

            checkPlayerInventoryOpened();
            buttonHintHandle();
        }
    }

    void interaction()
    {
        if(status.Equals("CLOSED"))
        {
            openChest();
            animator.Play("OpenAnim");
        }
        else
        {
            closeChest();
            animator.Play("CloseAnim");
        }
    }

    void openChest()
    {
        FindObjectOfType<Inventory_Crafting_Panel>().open();
        chestCanvas.SetActive(true);
        ActionLock.setActionLock("UI_OPENED");
        UnityEngine.Cursor.visible = true;
        status = "OPENED";
    }

    void closeChest()
    {
        FindObjectOfType<Inventory_Crafting_Panel>().close();
        chestCanvas.SetActive(false);
        ActionLock.setActionLock("UNLOCKED");
        UnityEngine.Cursor.visible = false;
        status = "CLOSED";
    }

    void checkPlayerInventoryOpened()   //in case you press tab in close the inventory crafting panel while chest inventory opened; it's not only the player inventory, but also the crafting tab
    {
        if (status.Equals("OPENED") && Inventory_Crafting_Panel.getInventoryStatus().Equals("CLOSED"))    
        {
            FindObjectOfType<Inventory_Crafting_Panel>().open();
            ActionLock.setActionLock("UI_OPENED");
            UnityEngine.Cursor.visible = true;
        }
    }

    void buttonHintHandle()
    {
        if (status.Equals("CLOSED"))
            Button_Hint.setBuildingInteractionHint("Open 'Chest'");
        else
            Button_Hint.clearBuildingInteractionHint();
    }

    GameObject getInventoryHolder()
    {
        return this.gameObject;
    }


    public ArrayList getSaveData()
    {
        return getInventoryHolder().GetComponent<Inventory>().getSaveData();
    }

    public void loadData(int[] itemCodeArray, int[] quantityArray, float[] chargeArray)
    {
        getInventoryHolder().GetComponent<Inventory>().loadData(itemCodeArray, quantityArray, chargeArray);
    }
}
