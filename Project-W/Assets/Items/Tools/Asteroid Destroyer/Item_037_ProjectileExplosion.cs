using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_037_ProjectileExplosion : MonoBehaviour
{
    float timeSinceSpawned;
    void Start()
    {
        timeSinceSpawned = 0;
    }


    void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned >= 10)
            Destroy(this.gameObject);
    }
}
