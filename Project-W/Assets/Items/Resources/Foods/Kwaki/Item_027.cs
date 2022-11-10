using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_027 : Item    //kwaki
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && itemCode == selectedItemCode && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
            consume();
    }

    private void consume()
    {
        int saturation = FindObjectOfType<Consumable_Data>().getSaturationIncrease(itemCode);
        int thirst = FindObjectOfType<Consumable_Data>().getThirstIncrease(itemCode);
        FindObjectOfType<Player_Stats>().changeSaturation(saturation);
        FindObjectOfType<Player_Stats>().changeThirst(thirst);
        int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
    }
}
