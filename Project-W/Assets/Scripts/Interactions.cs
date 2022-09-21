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

    void Update()
    {
        checkResources();
    }

    void checkResources()
    {
        Collider[] colliders = Physics.OverlapCapsule(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * detectionCapsuleLength, detectionCapsuleRadius, resourcesMask);
        if(colliders.Length > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (colliders[0].tag.Equals("Item_003"))   //dropped box
                {
                    int itemCode = colliders[0].GetComponent<Item_003>().getItemCode();
                    int quantity = colliders[0].GetComponent<Item_003>().getQuantity();
                    FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(itemCode, quantity);
                }
                else
                    FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(Item.getItemCode(colliders[0].tag), 1);

                Destroy(colliders[0].gameObject);
            }
        }
    }
}
