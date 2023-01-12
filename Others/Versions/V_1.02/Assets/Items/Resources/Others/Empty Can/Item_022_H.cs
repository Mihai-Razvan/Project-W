using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_022_H : Consumable     //empty can
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
