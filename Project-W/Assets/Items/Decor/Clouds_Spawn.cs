using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds_Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject cloudPrefab;
    float timeSinceSpawn;

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if(timeSinceSpawn >= 4)
        {
            timeSinceSpawn = 0;
            float randRotY = Random.Range(0, 360);
            Instantiate(cloudPrefab, Random.insideUnitSphere * 70 + new Vector3(0, 130, 0), Quaternion.Euler(0, randRotY, 0));
        }
    }
}
