using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_004 : Prefabs  //platform
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
        }
    }

    void checkMerge()   //checks if finds another platform to merge with
    {
        Vector3 sphereCenter = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance, Color.red);
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, checkMergeSphereRadius, mergeLayerMask);
        float minDistance = int.MaxValue;

        if (colliders.Length == 0)
            mergeColliderPoint = null;
      
        for(int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector3.Distance(sphereCenter, colliders[i].transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                mergeColliderPoint = colliders[i].gameObject;
            }    
        }

        if (mergeColliderPoint != null && mergeColliderPoint.transform.parent.tag.Equals("Item_004"))
        {
            switch(mergeColliderPoint.name)
            {
                case "TopPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(0, 0, 2);
                    break;
                case "BottomPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(0, 0, -2);
                    break;
                case "LeftPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(-2, 0, 0);
                    break;
                case "RightPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(2, 0, 0);
                    break;
            }
        }
    }
}
