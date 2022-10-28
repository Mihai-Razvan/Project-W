using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_015_H : Food      //patato
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}
