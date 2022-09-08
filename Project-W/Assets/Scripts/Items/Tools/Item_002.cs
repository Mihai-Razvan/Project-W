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
    Dictionary<GameObject, float> objectsList;   //the second attribute "float" represents the distance between rayStart and object
    List<GameObject> keyList;    //we use this to store the gameobjects that are keys for the above dictionary so we can delete entries from the dictionary safe 
     
    void Start()
    {
        itemCode = 2;
        laserSize = 0;
        laserState = "UNUSED";
        objectsList = new Dictionary<GameObject, float>();
        keyList = new List<GameObject>();
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

        if (colliders.Length != 0)
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!keyList.Contains(colliders[0].gameObject)) //it means we are in the frame when this object collided for the first time
                {
                    colliders[i].transform.position = rayStartPosition.position + rayStartPosition.forward * Vector3.Distance(rayStartPosition.position, colliders[i].transform.position);
                    objectsList.Add(colliders[i].gameObject, Vector3.Distance(rayStartPosition.position, colliders[i].transform.position));
                    keyList.Add(colliders[0].gameObject);
                }

                if (colliders[0].gameObject.GetComponent<Rigidbody>() != null)  
                    Destroy(colliders[0].gameObject.GetComponent<Rigidbody>());

                if(colliders.Length == 1)
                {
                    laserState = "RETRACTING";
                    laserSize = Vector3.Distance(colliders[0].transform.position, rayStartPosition.position);
                }         
            }

        for (int i = 0; i < keyList.Count; i++)
        {
            objectsList[keyList[i]] = Vector3.Distance(rayStartPosition.position, rayStartPosition.position + rayStartPosition.forward * (objectsList[keyList[i]] - laserExpansionSpeed * Time.deltaTime));
            keyList[i].transform.position = rayStartPosition.position + rayStartPosition.forward * objectsList[keyList[i]];
        }

        for(int i = 0; i < keyList.Count; i++)
            if (Vector3.Distance(keyList[i].transform.position, rayStartPosition.position) < 0.5f)
            {
                FindObjectOfType<Inventory>().addItem(keyList[i].tag, 1);
                objectsList.Remove(keyList[i]);
                GameObject objectToDestroy = keyList[i];
                keyList.Remove(keyList[i]);
                Destroy(objectToDestroy);
            }
    }


}
