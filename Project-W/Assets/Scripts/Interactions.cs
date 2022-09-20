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
                FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(colliders[0].tag, 1);
                Destroy(colliders[0].gameObject);
            }
        }
    }
}
