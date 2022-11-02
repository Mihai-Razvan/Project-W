using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_022_H : Food     //empty can
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }
}
