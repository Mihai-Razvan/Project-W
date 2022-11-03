using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_023_H : Food     //full can
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}

