using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : Item
{
    [SerializeField]
    protected GameObject prefab;          //the prefab it will spawn in your hands
    
    protected void displayPrefab()
    {
        if (getUsedObject() == null && checkSelected())
        {
            GameObject spawnedObject = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            setUsedObject(spawnedObject);
        }
    }
}
