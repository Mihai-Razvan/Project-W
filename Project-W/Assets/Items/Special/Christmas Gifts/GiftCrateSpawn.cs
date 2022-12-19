using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCrateSpawn : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;    //spawner is positioned over the player, having the same x and z coordinates
    [SerializeField]
    float spawnHeight;           //the height over the player objects are spawned
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    GameObject giftCratePrefab;
    float spawnTimeElapsed;      //the time passed since a resource was spawned

    void Start()
    {
        spawnTimeElapsed = 0f;
    }

    void Update()
    {
        spawnGiftCrate();
    }

    void spawnGiftCrate()
    {
        spawnTimeElapsed += Time.deltaTime;

        if (spawnTimeElapsed >= 1f)    //every second it choses if to spawn or not
        {
            int randVal = Random.Range(1, 30);   

            if (randVal == 1)
            {
                Vector2 randomPos = new Vector2(playerTransform.position.x, playerTransform.position.z) + Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPos = new Vector3(randomPos.x, playerTransform.position.y + spawnHeight, randomPos.y);
                float ranRot = Random.Range(0, 180);
                Instantiate(giftCratePrefab, spawnPos, Quaternion.Euler(ranRot, ranRot, ranRot));
            }

            spawnTimeElapsed = 0f;
        }
    }
}
