using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_018_H : Consumable      //backed patato
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}

