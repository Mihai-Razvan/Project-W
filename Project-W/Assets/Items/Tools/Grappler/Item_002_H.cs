using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002_H : Tool         //grappler
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
