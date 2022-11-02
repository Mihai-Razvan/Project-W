using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_021_P : Building           //water extractor
{
    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }

    void Update()
    {
        if (checkSelected() && getUsedObject() != null && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            placeDummy();
            rotateObject();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                spawnPlacePrefab();
        }
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
}