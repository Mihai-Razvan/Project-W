using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_017_H : Food      //battery
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}

