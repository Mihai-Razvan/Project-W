using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_037_H : Tool         //asteroid destroyer
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
