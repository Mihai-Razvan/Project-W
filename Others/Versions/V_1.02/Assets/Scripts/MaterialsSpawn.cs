using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsSpawn : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;    //spawner is positioned over the player, having the same x and z coordinates
    [SerializeField]
    float spawnHeight;           //the height over the player objects are spawned
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    float spawnTimeInterval;        //the interval between 2 resources spawn
    float spawnTimeElapsed;      //the time passed since a resource was spawned
    [SerializeField]
    GameObject[] resourcePrefabs;
    [SerializeField]
    int[] chance;
    

    void Start()
    {
        spawnTimeElapsed = 0f;
    }

    void Update()
    {
        spawnMaterial();
    }

    void spawnMaterial()
    {
        spawnTimeElapsed += Time.deltaTime;

        if(spawnTimeElapsed >= spawnTimeInterval)
        {
            Vector2 randomPos = new Vector2(playerTransform.position.x, playerTransform.position.z) + Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(randomPos.x, playerTransform.position.y + spawnHeight, randomPos.y);
            float ranRot = Random.Range(0, 180);
            
            Instantiate(resourcePrefabs[chooseMaterial()], spawnPos, Quaternion.Euler(ranRot, ranRot, ranRot));  

            spawnTimeElapsed = 0f;
        }
    }

    int chooseMaterial()
    {
        int randVal =  Random.Range(1, 101);
        int count = 0;
        
        for(int i = 0; i < chance.Length; i++)
        {
            count += chance[i];
            if (randVal <= count)
                return i;
        }

        return 0;
    }
}
