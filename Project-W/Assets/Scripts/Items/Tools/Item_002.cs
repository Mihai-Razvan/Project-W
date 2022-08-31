using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002 : Tool
{
    //grappler

    LineRenderer laserLine;
    float laserSize;   //the laser isn't full size since beginning, it is expanding during a few seconds; it starts from 0 and then increase, until reaches it final destination
    int maxLaserSize;
    [SerializeField]
    float laserExpansionSpeed;    //the speed at which the laser is expansing or retrating
    string laserState;
    Transform rayStartPosition;
     
    void Start()
    {
        itemCode = 2;
        laserSize = 0;
        maxLaserSize = 9;
        laserState = "UNUSED";
    }

    void Update()
    {
        if (checkSelected())
        {
            displayPrefab();
            rayStartPosition = getUsedObject().transform.GetChild(0).transform.GetChild(0);
            laserLine = getUsedObject().GetComponent<LineRenderer>();

            if (Input.GetKey(KeyCode.Mouse0))
            {
                drawLaser();
                makeCollider();
            }
            else
            {
                laserLine.positionCount = 0;     //so the ray disappears
                laserSize = 0;
            }
        }
    }

    void drawLaser()
    {
        
        laserLine.positionCount = 2;         //so the ray appears; if we don't do this after setting it to 0 rat won't display
        laserLine.SetPosition(0, rayStartPosition.position);
      
        laserLine.SetPosition(1, Camera.main.transform.position + Camera.main.transform.forward * laserSize);

        switch(laserState)
        {
            case "UNUSED":
                laserSize = laserSize + laserExpansionSpeed * Time.deltaTime;
                laserState = "EXPANDING";
                break;
            case "EXPANDING":
                if (laserSize < maxLaserSize)
                    laserSize = laserSize + laserExpansionSpeed * Time.deltaTime;
                else
                    laserState = "RETRACTING";
                break;
            case "RETRACTING":
                if (laserSize > 0)
                    laserSize = laserSize - laserExpansionSpeed * Time.deltaTime;
                else
                    laserState = "UNUSED";
                break;
        }
    }

    void makeCollider()
    {
        Collider[] colliders = Physics.OverlapCapsule(rayStartPosition.position, Camera.main.transform.position + Camera.main.transform.forward * laserSize, 1);

        for (int i = 0; i < colliders.Length; i++)
            Destroy(colliders[i].gameObject.GetComponent<Rigidbody>());
    }


}
