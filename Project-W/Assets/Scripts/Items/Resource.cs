using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    [SerializeField]
    float cloudLevel;    //the z coordinates for the cloud layer; if a ressource falls under this level it despawns
    [SerializeField]
    float fallVelocity;  //the speed at which the resources fall from the sky

    void Update()
    {
        if(TryGetComponent(out Rigidbody rigidBody) == true)
            rigidBody.velocity = new Vector3(0, fallVelocity, 0);

        if (transform.position.y < cloudLevel)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Structure"))
        {
            Destroy(GetComponent<Rigidbody>());
            GetComponent<BoxCollider>().isTrigger = true;
            Destroy(GetComponent<Resource>());
        }
    }
}
