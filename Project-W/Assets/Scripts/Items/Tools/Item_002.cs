using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002 : Tool
{
    //grappler

    LineRenderer laserLine;
    float laserSize;   //the laser isn't full size since beginning, it is expanding during a few seconds; it starts from 0 and then increase, until reaches it final destination
    [SerializeField]
    int maxLaserSize;
    [SerializeField]
    float laserExpansionSpeed;    //the speed at which the laser is expansing or retrating
    [SerializeField]
    float rayRadius;               //the radius for the capsule collider that si cheking for the ray collision
    string laserState;
    Transform rayStartPosition;
    [SerializeField]
    LayerMask resourcesMask;
     
    void Start()
    {
        itemCode = 2;
        laserSize = 0;
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
      
        laserLine.SetPosition(1, rayStartPosition.position + rayStartPosition.forward * laserSize);
        Debug.Log(laserSize);

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

    void makeCollider()    //creates and manages the collider around the ray to check for resources
    {
        Collider[] colliders = Physics.OverlapCapsule(rayStartPosition.position, rayStartPosition.position + rayStartPosition.forward * laserSize, rayRadius, resourcesMask);

        if(colliders.Length != 0)
            switch(laserState)
            {
                case "EXPANDING":  //if is expanding and we have a collision it means it is the only resources colliding since there are virtually 0 chances to have 2 resources colliding for the first time in the same frame
                    laserState = "RETRACTING";
                    laserSize = Vector3.Distance(colliders[0].transform.position, rayStartPosition.position);
                    colliders[0].transform.position = rayStartPosition.position + rayStartPosition.forward * laserSize;
                    Destroy(colliders[0].gameObject.GetComponent<Rigidbody>());
                    break;
                case "RETRACTING":
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].gameObject.GetComponent<Rigidbody>() != null)
                        {
                            Destroy(colliders[i].gameObject.GetComponent<Rigidbody>());
                            colliders[i].transform.position = rayStartPosition.position + rayStartPosition.forward * Vector3.Distance(rayStartPosition.position, colliders[i].transform.position);
                        }

                       colliders[i].transform.position = colliders[i].transform.position - rayStartPosition.forward * laserExpansionSpeed * Time.deltaTime;   //it gets closer everty frame

                        if(Vector3.Distance(colliders[i].transform.position, rayStartPosition.position) < 0.5f)
                        {
                            FindObjectOfType<Inventory>().addItem(colliders[i].tag, 1);
                            Destroy(colliders[i].gameObject);
                        }
                    }

                    break;
            }
    }


}
