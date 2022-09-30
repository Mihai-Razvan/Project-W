using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_006_P : Building     //pillar
{
    [SerializeField]
    float checkMergeDistance;
    [SerializeField]
    float checkMergeSphereRadius;
    GameObject mergeColliderPoint;      //the point to which we will merge
    [SerializeField]
    LayerMask mergeLayerMask;        //the layer which will be checked for merging with

    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }

    void Update()
    {
        if (checkSelected() && getUsedObject() != null && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            checkMerge();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                placeBuilding();
        }
    }

    void checkMerge()   //checks if finds a platform to be placed on
    {
        Vector3 sphereCenter = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, checkMergeSphereRadius, mergeLayerMask);
        float minDistance = int.MaxValue;

        if (colliders.Length == 0)
            mergeColliderPoint = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector3.Distance(sphereCenter, colliders[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                mergeColliderPoint = colliders[i].gameObject;
            }
        }

        if (mergeColliderPoint != null && mergeColliderPoint.transform.parent.gameObject.GetComponent<Prefab_Data>().getPrefabType().Equals("Foundation") && 
            mergeColliderPoint.gameObject.name.Equals("CenterPoint"))  //pillar can be put only on foundation TYPE (not item)   
            getUsedObject().transform.position = mergeColliderPoint.transform.position;
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.position + new Vector3(0, 3.2f, 0);
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, Quaternion.identity, buildingMask); 

        if (colliders.Length == 0)
            changeMaterials(placeableMaterial);
        else
            changeMaterials(notPlaceableMaterial);

        return colliders.Length;
    }
}
