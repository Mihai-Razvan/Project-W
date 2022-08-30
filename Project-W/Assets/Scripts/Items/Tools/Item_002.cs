using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002 : Tool
{
    //grappler
     
    void Start()
    {
        itemCode = 2;
    }

    void Update()
    {
        if (checkSelected())
            displayPrefab();
    }
}
