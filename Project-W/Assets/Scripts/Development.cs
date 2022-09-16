using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
            FindObjectOfType<Inventory>().addItem("Item_001", 1);
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
            FindObjectOfType<Inventory>().addItem("Item_002", 1);
    }
}
