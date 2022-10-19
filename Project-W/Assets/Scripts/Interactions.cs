using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField]
    float detectionCapsuleLength;
    [SerializeField]
    float detectionCapsuleRadius;
    [SerializeField]
    LayerMask resourcesMask;
    [SerializeField]
    LayerMask buildingsMask;

    GameObject interactingBuilding;   //the building the player can interact with; even if it s a non interactable building this will still hold the gameobject

    void Update()
    {
        checkResources();
        checkBuildings();
    }

    void checkResources()
    {
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * detectionCapsuleLength, detectionCapsuleRadius, resourcesMask);
        if(colliders.Length > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                int[] itemCodeArray = colliders[0].GetComponent<ResourcesData>().getItemCodeArray();
                int[] quantityArray = colliders[0].GetComponent<ResourcesData>().getQuantityArray();
                for (int j = 0; j < itemCodeArray.Length; j++)
                    FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCodeArray[j], quantityArray[j]);

                Destroy(colliders[0].gameObject);
            }
        }
    }

    void checkBuildings()
    {
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * detectionCapsuleLength, detectionCapsuleRadius, buildingsMask);
        if (colliders.Length > 0)
            interactingBuilding = colliders[0].gameObject;
        else
            interactingBuilding = null;
    }

    public GameObject getInteractingBuilding()
    {
        return interactingBuilding;
    }
}
