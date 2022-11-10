using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_020_P : Placeable //collection vacuum
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
        if (itemCode == selectedItemCode && getUsedObject() != null && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            checkMerge();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                spawnPlacePrefab();
        }
    }

    void checkMerge()   //checks if finds another platform to merge with
    {
        Vector3 capsuleEnd = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, capsuleEnd, checkMergeSphereRadius, mergeLayerMask);
        float minDistance = int.MaxValue;

        if (colliders.Length == 0)
            mergeColliderPoint = null;

        for (int i = 0; i < colliders.Length; i++)
            if (checkValidMerge(colliders[i].gameObject))
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
            switch (mergeColliderPoint.name)
            {
                case "UpPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(0, 0, 1.9f);
                    break;
                case "DownPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(0, 0, -1.9f);
                    break;
                case "LeftPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(-1.9f, 0, 0);
                    break;
                case "RightPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(1.9f, 0, 0);
                    break;
            }
        }
    }

    bool checkValidMerge(GameObject mergePoint)
    {
        return mergePoint.transform.parent.tag.Equals("Item_004") || mergePoint.transform.parent.tag.Equals("Item_020");
        //collection vacuum can be attached only to another collection vacuum or foundation 
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.position;
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity, buildingMask);

        if (colliders.Length == 0)
            changeMaterials(placeableMaterial);
        else
            changeMaterials(notPlaceableMaterial);

        return colliders.Length;
    }
}

