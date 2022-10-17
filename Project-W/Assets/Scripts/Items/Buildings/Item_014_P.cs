using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_014_P : Building           //crop plot
{
    [SerializeField]
    float checkFoundationDistance;

    void Start()
    {
        Player_Inventory.onItemSelected += displayPrefab;
    }

    void Update()
    {
        if (checkSelected() && getUsedObject() != null && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            placement();
            rotateObject();
            if (checkCollision() == 0 && Input.GetKeyDown(KeyCode.Mouse0))
                placeBuilding();
        }
    }

    void placement()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, checkFoundationDistance, buildingMask);
        for(int i = 0; i < hits.Length; i++)
            if(hits[i].collider.TryGetComponent(out Prefab_Data data) && data.getPrefabType().Equals("Foundation"))
            {
                getUsedObject().transform.position = hits[i].point; 
                break;
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

    void rotateObject()
    {
        if (Input.GetKey(KeyCode.R))
            getUsedObject().transform.rotation *= Quaternion.Euler(0, 90 * Time.deltaTime, 0);
    }
}
