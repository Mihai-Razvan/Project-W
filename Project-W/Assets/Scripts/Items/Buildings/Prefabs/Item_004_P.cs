using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_004_P : Building  //foundation
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

    void checkMerge()   //checks if finds another platform to merge with
    {
        Vector3 sphereCenter = Camera.main.transform.position + Camera.main.transform.forward * checkMergeDistance;
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

        if (mergeColliderPoint != null && mergeColliderPoint.transform.parent.tag.Equals("Item_004"))  //foundation can be attached only to another foundation
        {
            switch(mergeColliderPoint.name)
            {
                case "TopPoint":
                    getUsedObject().transform.position = mergeColliderPoint.transform.position + new Vector3(0, 0, 1.9f);
                    break;
                case "BottomPoint":
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

    void changeMaterials(Material material)
    {
        Renderer usedObjectRenderer = getUsedObject().transform.GetChild(0).gameObject.GetComponent<Renderer>();
        Material[] materials = new Material[usedObjectRenderer.materials.Length];

        for (int i = 0; i < materials.Length; i++)
            materials[i] = material;

        usedObjectRenderer.materials = materials;
    }
}
