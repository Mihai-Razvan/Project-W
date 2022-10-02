using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_008_P : Building    //stairs
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
            rotateObject();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                placeBuilding();
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
            switch (getUsedObject().transform.rotation.eulerAngles.y)
            {
                case 0:
                    mergeColliderPoint = mergeColliderPoint.transform.parent.transform.Find("LeftPoint").gameObject;
                    break;
                case 180:
                    mergeColliderPoint = mergeColliderPoint.transform.parent.transform.Find("RightPoint").gameObject;
                    break;
                case 90:
                    mergeColliderPoint = mergeColliderPoint.transform.parent.transform.Find("DownPoint").gameObject;
                    break;
                case 270:
                    mergeColliderPoint = mergeColliderPoint.transform.parent.transform.Find("UpPoint").gameObject;
                    break;
            }

            getUsedObject().transform.position = mergeColliderPoint.transform.position;
        }
    }

    bool checkValidMerge(GameObject mergePoint)
    {
        return mergePoint.transform.parent.GetComponent<Prefab_Data>().getPrefabType().Equals("Foundation") && !mergePoint.name.Equals("CenterPoint"); //floor can be attached only foundation type
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.GetChild(0).transform.position;
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, getUsedObject().transform.rotation, buildingMask);
      
        if (colliders.Length == 0)
            changeMaterials(placeableMaterial);
        else
            changeMaterials(notPlaceableMaterial);

        return colliders.Length;
    }

    void rotateObject()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            getUsedObject().transform.rotation *= Quaternion.Euler(0, 90, 0);
            
        }
    }
}
