using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_005_P : Building   //wall
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

    void checkMerge()   //checks if finds a platform to merge with
    {
        Vector3 capsuleEnd = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, capsuleEnd, checkMergeSphereRadius, mergeLayerMask);
        float minDistance = int.MaxValue;

        if (colliders.Length == 0)
            mergeColliderPoint = null;

        for (int i = 0; i < colliders.Length; i++)
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
            if (mergeColliderPoint.transform.parent.gameObject.GetComponent<Prefab_Data>().getPrefabType().Equals("Foundation") && !mergeColliderPoint.gameObject.name.Equals("CenterPoint"))
            {
                getUsedObject().transform.position = mergeColliderPoint.transform.position;
                if (mergeColliderPoint.name.Equals("UpPoint") || mergeColliderPoint.name.Equals("DownPoint"))
                    getUsedObject().transform.rotation = Quaternion.Euler(0, 90, 0);
                else
                    getUsedObject().transform.rotation = Quaternion.identity;
            }
            else if (mergeColliderPoint.transform.parent.tag.Equals("Item_005"))
            {
                getUsedObject().transform.position = mergeColliderPoint.transform.position;
                getUsedObject().transform.rotation = mergeColliderPoint.transform.parent.transform.rotation;
            }
        }
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.position + new Vector3(0, 3, 0);
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, getUsedObject().transform.rotation, buildingMask);
        Collider[] wallColliders = Physics.OverlapBox(boxCenter, boxSize / 4f, Quaternion.identity, buildingMask);  // to solve the collision problems with other walls

        bool placeable = (wallColliders.Length == 0);
        int numOfOtherCollieders = 0;
        
        for(int i = 0; i < colliders.Length; i++)
            if(colliders[i].gameObject.tag != "Item_005")
            {
                placeable = false;
                numOfOtherCollieders++;
            }
    
        if (placeable == true)
            changeMaterials(placeableMaterial);
        else
            changeMaterials(notPlaceableMaterial);
     
        return wallColliders.Length + numOfOtherCollieders;
    }
}
