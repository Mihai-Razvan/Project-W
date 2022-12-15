using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_007_P : Placeable       //floor
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
        if (itemCode == selectedItemCode && getUsedObject() != null && ActionLock.getActionLock().Equals("UNLOCKED"))
        {
            checkMerge();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                spawnPlacePrefab();
        }
    }

    void checkMerge()   //checks if finds a pillar to merge with
    {
        Vector3 capsuleEnd = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, capsuleEnd, checkMergeSphereRadius, mergeLayerMask);
        float minDistance = int.MaxValue;

        if (colliders.Length == 0)
            mergeColliderPoint = null;

        for (int i = 0; i < colliders.Length; i++)
            if(checkValidMerge(colliders[i].gameObject))
            {
                float distance = Vector3.Distance(Camera.main.transform.position, colliders[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    mergeColliderPoint = colliders[i].gameObject;
                }
            }

        if (mergeColliderPoint != null)  
        {
            getUsedObject().transform.position = mergeColliderPoint.transform.position;
        }
    }

    bool checkValidMerge(GameObject mergePoint)
    {
        return mergePoint.transform.parent.tag.Equals("Item_006"); //floor can be attached only to a pillar
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.position;
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, Quaternion.identity, buildingMask);

        if (colliders.Length == 0)
            changeMaterials(placeableMaterial);
        else
            changeMaterials(notPlaceableMaterial);

        return colliders.Length;
    }

    void OnDestroy()
    {
        Player_Inventory.onItemSelected -= displayPrefab;
    }
}
