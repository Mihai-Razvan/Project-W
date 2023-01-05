using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    [SerializeField]
    float spawnInterval;
    [SerializeField]
    GameObject asteroidPrefab;
    [SerializeField]
    LayerMask placeableMask;
    float timeSinceSpawned;
    [SerializeField]
    float spawnHeight;
    
    void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if(timeSinceSpawned >= spawnInterval)
        {
            spawn();
            timeSinceSpawned = 0;
        }
    }

    void spawn()
    {
        bool foundPoint = false;
        Vector3 hitPoint = new Vector3();
        GameObject hitObject = new GameObject();

        if (GameObject.FindGameObjectsWithTag("Item_004").Length == 0)      //in case there are no foundations left it won't try to spawn asteroids; if we don't do this it will enter in an infinite loop because it won't find a hitPoint
            return;

        while(foundPoint == false)
        {
            Vector3 point = Random.insideUnitSphere * 70 + new Vector3(0, 150, 0);
            RaycastHit[] hits = Physics.RaycastAll(point, -transform.up, 1000, placeableMask);

            for (int i = 0; i < hits.Length; i++)
                if(hits[i].collider.gameObject.tag.Equals("Item_004"))  //foundation
                {
                    hitPoint = hits[i].point;
                    hitObject = hits[i].collider.gameObject;
                    foundPoint = true;
                    break;
                }
        }

        float randXDistance = Random.Range(-150, 150);
        float randZDistance = Random.Range(-150, 150);
        Vector3 spawnPoint = hitPoint + new Vector3(randXDistance, spawnHeight, randZDistance);

        GameObject spawnedObject = Instantiate(asteroidPrefab, spawnPoint, Quaternion.identity);
        spawnedObject.GetComponent<Asteroid>().setHitPoint(hitPoint);
        spawnedObject.GetComponent<Asteroid>().setHitObject(hitObject);
    }
}
