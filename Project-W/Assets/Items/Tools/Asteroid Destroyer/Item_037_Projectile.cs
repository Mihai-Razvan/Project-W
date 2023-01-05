using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_037_Projectile : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject asteroidHitPrefab;
    [SerializeField]
    LayerMask asteroidMask;
    [SerializeField]
    float collisionSphereRadius;
    float timeSinceSpawned;
    [SerializeField]
    GameObject projectileExplosionPrefab;

    void Start()
    {
        timeSinceSpawned = 0;
    }

    void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned >= 5)
        {
            Instantiate(projectileExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        checkAsteroidCollision();
    }

    void checkAsteroidCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionSphereRadius, asteroidMask);

        if(colliders.Length > 0)
        {
            Instantiate(asteroidHitPrefab, colliders[0].gameObject.transform.position, Quaternion.identity);
            Instantiate(projectileExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(colliders[0].gameObject);
            FindObjectOfType<SoundsManager>().playAsteroidExplosionSound();
            Destroy(this.gameObject);
        }
    }
}
