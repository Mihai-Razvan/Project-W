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
    List<GameObject> objectList;
    List<float> distanceList;

     
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
        Player_Inventory.onItemDeselected += deselectItem;

        laserSize = 0;
        laserState = "UNUSED";
        chargeTime = 0;
        objectList = new List<GameObject>();
        distanceList = new List<float>();
    }

    void Update()
    {
        if (itemCode == selectedItemCode && getUsedObject() != null && ActionLock.getActionLock().Equals("UI_OPENED") == false)     //we use getusedobject() in case the object appears on selected slot when slot is already selected
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
                        ActionLock.setActionLock("ACTION_LOCKED");
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        laserState = "EXPANDING";
                        ChargeRadial.resetCharge();
                        ActionLock.setActionLock("ACTION_LOCKED");
                    }
                    else
                        beamStartEffect.SetActive(false);

                    break;
                case "EXPANDING":
                    drawLaser();
                    makeCollider();
                    moveMaterials();
                    ActionLock.setActionLock("ACTION_LOCKED");

                    break;
                case "RETRACTING":
                    drawLaser();
                    makeCollider();
                    moveMaterials();
                    ActionLock.setActionLock("ACTION_LOCKED");

                    if (laserState.Equals("UNUSED"))   //in case it became UNUSED in drawLaser()
                    {
                        chargeTime = 0;
                        laserLine.positionCount = 0;     //so the ray disappears
                        laserSize = 0;
                        ActionLock.setActionLock("UNLOCKED");
                    }

                    break;
            }
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
                if (objectList.Count != 0)         //if we are retracting objects the retraction speed is slower
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

        if (colliders.Length != 0 && !colliders[0].tag.Equals("Item_003"))    //since we destroy the object's collider when it collides for the first time, if we have multiple retracting objects it won't collide with them anymore
        {
            if (colliders[0].gameObject.TryGetComponent(out Rigidbody rigidBody) == true)
            {
                Destroy(rigidBody);
                Destroy(colliders[0].gameObject.GetComponent<BoxCollider>());
            }
            else
                return;   //it means it is in collection vacuum

            colliders[0].transform.position = rayStartPosition.position + rayStartPosition.forward * Vector3.Distance(rayStartPosition.position, colliders[0].transform.position);
            colliders[0].gameObject.AddComponent<Outline>();      //it adds the script that creates the encapsulate outline
            objectList.Add(colliders[0].gameObject);
            distanceList.Add(Vector3.Distance(rayStartPosition.position, colliders[0].transform.position));

            if (laserState.Equals("EXPANDING"))    //it means it is the first object we colided with
            {
                laserState = "RETRACTING";
                laserSize = Vector3.Distance(colliders[0].transform.position, rayStartPosition.position);
            }
        }
    }

    void moveMaterials()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            distanceList[i] = Vector3.Distance(rayStartPosition.position, rayStartPosition.position + rayStartPosition.forward * (distanceList[i] - laserExpansionSpeed * Time.deltaTime * laserRetractingSlowDown));
            objectList[i].transform.position = rayStartPosition.position + rayStartPosition.forward * distanceList[i];
        }

        for (int i = 0; i < objectList.Count; i++)
            if (Vector3.Distance(objectList[i].transform.position, rayStartPosition.position) < 0.5f)
            {
                int[] itemCodeArray = objectList[i].GetComponent<ResourcesData>().getItemCodeArray();
                int[] quantityArray = objectList[i].GetComponent<ResourcesData>().getQuantityArray();
                float[] chargeArray = objectList[i].GetComponent<ResourcesData>().getChargeArray();
                for (int j = 0; j < itemCodeArray.Length; j++)
                    Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCodeArray[j], quantityArray[j], chargeArray[j]);

                Destroy(objectList[i]);
                objectList.RemoveAt(i);
                distanceList.RemoveAt(i);
                return;
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
