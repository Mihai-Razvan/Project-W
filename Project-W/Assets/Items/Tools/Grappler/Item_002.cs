using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_002 : Tool   //grappler
{
    LineRenderer laserLine;
    float laserSize;   //the laser isn't full size since beginning, it is expanding during a few seconds; it starts from 0 and then increase, until reaches it final destination
    [SerializeField]
    int laserSizePerSecond;    //the size laser expands for every second charged; for ex if this value is 10 and you charge 2.5 sec laser will go 25
    [SerializeField]
    float maxChargeTime;
    float chargeTime;
    [SerializeField]
    float laserExpansionSpeed;    //the speed at which the laser is expansing or retrating
    [SerializeField]
    float laserRetractingSlowDown;     //retracting is slower when we got object retracting
    [SerializeField]
    float rayRadius;               //the radius for the capsule collider that si cheking for the ray collision
    string laserState;
    Transform rayStartPosition;
    [SerializeField]
    LayerMask resourcesMask;
    GameObject beamStartEffect;
    Dictionary<GameObject, float> objectsList;   //the second attribute "float" represents the distance between rayStart and object
    List<GameObject> keyList;    //we use this to store the gameobjects that are keys for the above dictionary so we can delete entries from the dictionary safe 

     
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
        Player_Inventory.onItemDeselected += deselectItem;

        laserSize = 0;
        laserState = "UNUSED";
        chargeTime = 0;
        objectsList = new Dictionary<GameObject, float>();
        keyList = new List<GameObject>();
    }

    void Update()
    {
        if (checkSelected() && getUsedObject() != null && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))     //we use getusedobject() in case the object appears on selected slot when slot is already selected
        {
            beamStartEffect = getUsedObject().transform.GetChild(0).transform.GetChild(1).gameObject;
            rayStartPosition = getUsedObject().transform.GetChild(0).transform.GetChild(0);
            laserLine = getUsedObject().GetComponent<LineRenderer>();

            switch (laserState)
            {
                case "UNUSED":
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        chargeLaser();
                        beamStartEffect.SetActive(true);
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        laserState = "EXPANDING";
                        FindObjectOfType<ChargeRadial>().resetCharge();
                    }
                    else
                        beamStartEffect.SetActive(false);

                    break;
                case "EXPANDING":
                    drawLaser();
                    makeCollider();
                    moveMaterials();

                    break;
                case "RETRACTING":
                    drawLaser();
                    makeCollider();
                    moveMaterials();

                    if (laserState.Equals("UNUSED"))   //in case it became UNUSED in drawLaser()
                    {
                        chargeTime = 0;
                        laserLine.positionCount = 0;     //so the ray disappears
                        laserSize = 0;
                    }

                    break;
            }

            if (chargeTime != 0)
                FindObjectOfType<Player>().setActionLock("ACTION_LOCKED");   //if chargeTime != 0 it means it is expanding, retracting or charging, so action is locked
            else
                FindObjectOfType<Player>().setActionLock("UNLOCKED");
        }
      
    }

    void chargeLaser()
    {
        chargeTime += Time.deltaTime;

        if(chargeTime >= maxChargeTime)
            chargeTime = maxChargeTime;    //in case charge time is greater

        FindObjectOfType<ChargeRadial>().setCharge(chargeTime, maxChargeTime);
    }

    void drawLaser()
    {
        
        laserLine.positionCount = 2;         //so the ray appears; if we don't do this after setting it to 0 rat won't display
        laserLine.SetPosition(0, rayStartPosition.position);

        laserLine.SetPosition(1, rayStartPosition.position + rayStartPosition.forward * laserSize);

        if (laserState.Equals("EXPANDING"))
        {
            if (laserSize < laserSizePerSecond * chargeTime)
                laserSize = laserSize + laserExpansionSpeed * Time.deltaTime;
            else
                laserState = "RETRACTING";
        }
        else
        {
            if (laserSize > 0)
            {
                float slowDown = 1;
                if (keyList.Count != 0)         //if we are retracting objects the retraction speed is slower
                    slowDown = laserRetractingSlowDown;

                laserSize = laserSize - laserExpansionSpeed * Time.deltaTime * slowDown;
            }
            else
                laserState = "UNUSED";
        }
        
    }

    void makeCollider()    //creates and manages the collider around the ray to check for resources
    {
        Collider[] colliders = Physics.OverlapCapsule(rayStartPosition.position, rayStartPosition.position + rayStartPosition.forward * laserSize, rayRadius, resourcesMask);

        if (colliders.Length != 0)
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[0].gameObject.TryGetComponent(out Rigidbody rigidBody) == true)
                    Destroy(rigidBody);
                else
                    return;   //it means it is in collection vacuum

                if (!keyList.Contains(colliders[i].gameObject)) //it means we are in the frame when this object collided for the first time
                {
                    colliders[i].transform.position = rayStartPosition.position + rayStartPosition.forward * Vector3.Distance(rayStartPosition.position, colliders[i].transform.position);
                    objectsList.Add(colliders[i].gameObject, Vector3.Distance(rayStartPosition.position, colliders[i].transform.position));
                    keyList.Add(colliders[i].gameObject);
                         colliders[i].gameObject.AddComponent<Outline>();      //it adds the script that creates the encapsulate outline
                }

                if(colliders.Length == 1)
                {
                    laserState = "RETRACTING";
                    laserSize = Vector3.Distance(colliders[0].transform.position, rayStartPosition.position);
                }         
            }    
    }

    void moveMaterials()
    {
        for (int i = 0; i < keyList.Count; i++)
            if (Vector3.Distance(keyList[i].transform.position, rayStartPosition.position) < 0.5f)
            {
                int[] itemCodeArray = keyList[i].GetComponent<ResourcesData>().getItemCodeArray();
                int[] quantityArray = keyList[i].GetComponent<ResourcesData>().getQuantityArray();
                //   float[] chargeArray = keyList[i].GetComponent<ResourcesData>().getChargeArray();
                for (int j = 0; j < itemCodeArray.Length; j++)
                    FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCodeArray[j], quantityArray[j]);

                objectsList.Remove(keyList[i]);
                GameObject objectToDestroy = keyList[i];
                keyList.Remove(keyList[i]);
                Destroy(objectToDestroy);
            }

        for (int i = 0; i < keyList.Count; i++)
        {
            objectsList[keyList[i]] = Vector3.Distance(rayStartPosition.position, rayStartPosition.position + rayStartPosition.forward * (objectsList[keyList[i]] - laserExpansionSpeed * Time.deltaTime * laserRetractingSlowDown));
            keyList[i].transform.position = rayStartPosition.position + rayStartPosition.forward * objectsList[keyList[i]];
        }
    }

    void deselectItem()
    {
        chargeTime = 0;  
        laserSize = 0;
    }

    public float getChargeTime()
    {
        return chargeTime;
    }

    public float getMaxChargeTime()
    {
        return maxChargeTime;
    }

    public string getLaserState()
    {
        return laserState;
    }
}
