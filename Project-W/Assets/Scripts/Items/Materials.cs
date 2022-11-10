using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Item
{
    [SerializeField]
    float despawnLevel;    //the z coordinates for the cloud layer; if a ressource falls under this level it despawns
    [SerializeField]
    float fallVelocity;  //the speed at which the resources fall from the sky
    [SerializeField]
    LayerMask buildingMask;

    void Update()
    {
        if (TryGetComponent(out Rigidbody rb))
            rb.velocity = new Vector3(0, fallVelocity, 0);

        if (transform.position.y < despawnLevel)
            Destroy(gameObject);
    }
}
