using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(1, 1, 0);
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(2, 1, 0);
    }
}
