using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 hitPoint;
    [SerializeField]
    float collisionSphereRadius;
    [SerializeField]
    LayerMask placeableMask;

    void Start()
    {
        transform.LookAt(hitPoint);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint, speed * Time.deltaTime);
        checkCollision();

        if (Vector3.Distance(transform.position, hitPoint) <= 0.3f)
            Destroy(this.gameObject);
    }

    void checkCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionSphereRadius, placeableMask);

        for(int i = 0; i < colliders.Length; i++)
            Destroy(colliders[i].gameObject);
    }
    
    public void setHitPoint(Vector3 point)
    {
        hitPoint = point;
    }
}
