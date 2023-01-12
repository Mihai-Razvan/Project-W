using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_035_H : Tool         //frozen grappler
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }

    void OnDestroy()
    {
        Player_Inventory.onItemSelected -= displayPrefab;
    }
}

