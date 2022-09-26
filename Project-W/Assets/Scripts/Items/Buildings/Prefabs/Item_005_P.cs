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

    void checkMerge()   //checks if finds another platform to merge with
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

        if (mergeColliderPoint != null && mergeColliderPoint.transform.parent.gameObject.GetComponent<Prefab_Data>().getPrefabType().Equals("Foundation"))  //wall can be put only on foundation TYPE (not item)   
        {
            getUsedObject().transform.position = mergeColliderPoint.transform.position;
            if (mergeColliderPoint.name.Equals("TopPoint") || mergeColliderPoint.name.Equals("BottomPoint"))
                getUsedObject().transform.rotation = Quaternion.Euler(0, 90, 0);
            else
                getUsedObject().transform.rotation = Quaternion.identity;
        }
    }

    int checkCollision()       //it returns the number of colliding objects, and also handels the green/red materials switch
    {
        Vector3 boxCenter = getUsedObject().transform.position + new Vector3(0, 2.5f, 0);
        Vector3 boxSize = placePrefab.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2.5f, Quaternion.identity, buildingMask);

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
