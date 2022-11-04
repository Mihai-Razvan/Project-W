using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_027_H : Food      //kwaki
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}
