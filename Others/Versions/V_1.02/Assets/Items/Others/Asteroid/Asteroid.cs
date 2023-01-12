using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 hitPoint;
    GameObject hitObject;      //we also want to know the foundation that "contains" the hit point because if the asteroid spanws and while is falling it the foundation gets destroyed by another asteroid, this one won't be destroyed
    //when it reaches its hit point if its hitObject was already destroyed, and it will continues to fall until it will be despawned under despawnLevel
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

    [SerializeField]
    AudioSource asteroidSound;
    [SerializeField]
    float maxAsteroidSoundVolume;

    void Start()
    {
        transform.LookAt(hitPoint);
        playerAlreadyHit = false;

        changeAsteroidSoundVolume(FindObjectOfType<SoundsManager>().getSFxVolume());
        SoundsManager.onSFxVolumeChange += changeAsteroidSoundVolume;
    }

    void Update()
    {
        if (hitObject != null)
            transform.position = Vector3.MoveTowards(transform.position, hitPoint, speed * Time.deltaTime);
        else
        {
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;  //if the hitObject was destroyed the asteroid continues to fall under the hitPoint

            if (transform.position.y <= despawnLevel)  //normally the asteroid should hit platforms and be destroyed, but if smth strange happens and it doesn't get destroyed we destroy it under the clouds
                Destroy(this.gameObject);
        }

        checkPlaceablesCollision();
        checkPlayerCollsion();
    }

    void checkPlaceablesCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, collisionSphereRadius, placeableMask);
        bool hitObjectGetsDestroyed = false;    //if the hitObject gets destroyed in the loop lower; when it gets destroyed, also the asteroid gets destroyed

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == hitObject)
                hitObjectGetsDestroyed = true;

            Instantiate(asteroidHitPrefab, colliders[i].gameObject.transform.position, Quaternion.identity);
            Destroy(colliders[i].gameObject);
        }

        if (hitObjectGetsDestroyed == true)
        {
            FindObjectOfType<SoundsManager>().playAsteroidExplosionSound();
            Destroy(this.gameObject);
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

    public void setHitObject(GameObject obj)
    {
        hitObject = obj;
    }

    void changeAsteroidSoundVolume(float volume)
    {
        asteroidSound.volume = maxAsteroidSoundVolume * volume;
    }

    void OnDestroy()
    {
        SoundsManager.onSFxVolumeChange -= changeAsteroidSoundVolume;
    }
}
