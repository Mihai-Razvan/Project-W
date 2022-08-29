using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSpawn : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;    //spawner is positioned over the player, having the same x and z coordinates
    [SerializeField]
    float spawnHeight;           //the height over the player objects are spawned
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    float spawnFrequency;        //the interval between 2 resources spawn
    float spawnTimeElapsed;      //the time passed since a resource was spawned
    [SerializeField]
    GameObject[] resourcePrefabs;
    

    void Start()
    {
        spawnTimeElapsed = 0f;
    }

    void Update()
    {
        spawnResource();
    }

    void spawnResource()
    {
        spawnTimeElapsed += Time.deltaTime;

        if(spawnTimeElapsed >= 1f)
        {
            Vector2 randomPos = new Vector2(playerTransform.position.x, playerTransform.position.z) + Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(randomPos.x, playerTransform.position.y + spawnHeight, randomPos.y);
            Instantiate(resourcePrefabs[0], spawnPos, Quaternion.Euler(0, 0, 0));

            spawnTimeElapsed = 0f;
        }
    }
}
