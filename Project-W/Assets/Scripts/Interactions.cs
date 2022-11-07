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
    LayerMask placeablesMask;

    GameObject inRangeBuilding;   //the building the player CAN interact with; even if it s a non interactable building this will still hold the gameobject

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
                for(int i = 0; i < colliders.Length; i++)
                {
                    int[] itemCodeArray = colliders[i].GetComponent<ResourcesData>().getItemCodeArray();
                    int[] quantityArray = colliders[i].GetComponent<ResourcesData>().getQuantityArray();
                    float[] chargeArray = colliders[i].GetComponent<ResourcesData>().getChargeArray();
                    for (int j = 0; j < itemCodeArray.Length; j++)
                        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCodeArray[j], quantityArray[j], chargeArray[j]);

                    Destroy(colliders[i].gameObject);
                }
        }
    }

    void checkBuildings()
    {
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * detectionCapsuleLength, detectionCapsuleRadius, placeablesMask);
        if (colliders.Length > 0)
        {
            float minDistance = int.MaxValue;
            int minIndex = 0;

            for(int i = 0; i < colliders.Length; i++)
                if(colliders[i].gameObject.GetComponent<Placeable_Data>().getPlaceableType().Equals("Building")
                    && Vector3.Distance(colliders[i].gameObject.transform.position, Camera.main.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(colliders[i].gameObject.transform.position, Camera.main.transform.position);
                    minIndex = i;
                }
            inRangeBuilding = colliders[minIndex].gameObject;
        }
        else
            inRangeBuilding = null;
    }

    public GameObject getInRangeBuilding()   
    {
        return inRangeBuilding;
    }
}
