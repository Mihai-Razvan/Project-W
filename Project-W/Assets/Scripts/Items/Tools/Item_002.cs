using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002 : Tool
{
    //grappler

    LineRenderer laserLine;
     
    void Start()
    {
        itemCode = 2;
    }

    void Update()
    {
        if (checkSelected())
        {
            displayPrefab();
            if (Input.GetKey(KeyCode.Mouse0))
                use(); 
        }
    }

    void use()
    {
        Transform rayStartPosition = getUsedObject().transform.GetChild(1);

        laserLine = getUsedObject().GetComponent<LineRenderer>();
        laserLine.SetPosition(0, rayStartPosition.position);
        laserLine.SetPosition(1, rayStartPosition.forward * 10);
    }
}
