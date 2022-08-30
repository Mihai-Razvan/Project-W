using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]
    float cloudLevel;    //the z coordinates for the cloud layer; if a ressource falls under this level it despawns

    void Update()
    {
        if (transform.position.y < cloudLevel)
            Destroy(this.gameObject);
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
