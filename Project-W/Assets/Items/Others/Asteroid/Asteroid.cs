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
    [SerializeField]
    GameObject asteroidHitPrefab;
    [SerializeField]
    float playerDistanceDamage;        //the distance at which player has to be from the asteroid to get damaged
    [SerializeField]
    float playerHitDamage;           //the damage player gets when hit by the asteroid
    [SerializeField]
    float despawnLevel;
    bool playerAlreadyHit;       //so the player doesn't get damage in consecutive frames; player can get damage only once by an asteroid

    void Start()
    {
        transform.LookAt(hitPoint);
        playerAlreadyHit = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, hitPoint, speed * Time.deltaTime);
        checkPlaceablesCollision();
        checkPlayerCollsion();

        if (Vector3.Distance(transform.position, hitPoint) <= 0.05f)
            Destroy(this.gameObject);

        if (transform.position.y <= despawnLevel)  //normally the asteroid should hit platforms and be destroyed, but if smth strange happens and it doesn't get destroyed we destroy it under the clouds
            Destroy(this.gameObject);
    }

    void checkPlaceablesCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionSphereRadius, placeableMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Instantiate(asteroidHitPrefab, colliders[i].gameObject.transform.position, Quaternion.identity);
            Destroy(colliders[i].gameObject);
        }
    }

    void checkPlayerCollsion()
    {
        if (playerAlreadyHit)
            return;

        float distance = Vector3.Distance(this.gameObject.transform.position, FindObjectOfType<Player>().getPlayerPosition());

        if (distance <= playerDistanceDamage)
        {
            FindObjectOfType<Player_Stats>().changeHealth(-playerHitDamage);
            playerAlreadyHit = true;
        }
    }
    
    public void setHitPoint(Vector3 point)
    {
        hitPoint = point;
    }
}
