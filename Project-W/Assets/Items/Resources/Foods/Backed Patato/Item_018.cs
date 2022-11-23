using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_018 : Item  //backed patato
{
    void Update()
    {
        if (itemCode == selectedItemCode && Player.getActionLock().Equals("INVENTORY_OPENED") == false)
        {
            Button_Hint.setConsumeInteractionHint("Eat\n'Backed Patato'");

            if (Input.GetKeyDown(KeyCode.Mouse0))
                consume();
        }
    }

    private void consume()
    {
        int saturation = FindObjectOfType<Consumable_Data>().getSaturationIncrease(itemCode);
        int thirst = FindObjectOfType<Consumable_Data>().getThirstIncrease(itemCode);
        FindObjectOfType<Player_Stats>().changeSaturation(saturation);
        FindObjectOfType<Player_Stats>().changeThirst(thirst);
        int playerInventorySlot = Player_Inventory.getSelectedSlot();
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
    }
}

