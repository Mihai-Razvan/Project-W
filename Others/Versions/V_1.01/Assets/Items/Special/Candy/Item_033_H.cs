using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_033_H : Consumable      //candy
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
