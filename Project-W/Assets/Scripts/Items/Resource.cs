using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Item
{
    [SerializeField]
    float cloudLevel;    //the z coordinates for the cloud layer; if a ressource falls under this level it despawns
    [SerializeField]
    float fallVelocity;  //the speed at which the resources fall from the sky
    float time;
    bool hasCollided;
    bool removedRB;
    float timeSinceCollision;
    [SerializeField]
    LayerMask buildingMask;

    void Update()
    {
        if(hasCollided && removedRB == false)
        {
            timeSinceCollision += Time.deltaTime;
            if (timeSinceCollision >= 2f)      
            {
                Collider[] colliders = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size, transform.rotation, buildingMask);
                if (colliders.Length == 0)
                {
                    Destroy(GetComponent<Rigidbody>());
                    GetComponent<BoxCollider>().isTrigger = true;
                    removedRB = true;
                }
                else
                    hasCollided = false;
            }
        }

        if (removedRB == false)
            GetComponent<Rigidbody>().velocity = new Vector3(0, fallVelocity, 0);
        else
        {
            time += Time.deltaTime;
            if (time >= 60)                //resources despawns after 1 min
                Destroy(this.gameObject);
        }

        if (transform.position.y < cloudLevel)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8) //Building layer
            hasCollided = true;
    }
}
