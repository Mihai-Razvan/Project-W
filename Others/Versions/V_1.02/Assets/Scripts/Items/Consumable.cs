using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    [SerializeField]
    protected GameObject prefab;          //the prefab it will spawn in your hands
    [SerializeField]
    protected GameObject parentObject;    //the position your prefab will be spawned at (the position is the gameobject "Hand" attached to camera in the scene for all tools)

    protected void displayPrefab()
    {
        if (itemCode == selectedItemCode && getUsedObject() == null)
        {
            GameObject spawnedObject = Instantiate(prefab, parentObject.transform.position, Quaternion.Euler(parentObject.transform.eulerAngles.x, parentObject.transform.eulerAngles.y, parentObject.transform.eulerAngles.z));
            spawnedObject.transform.SetParent(parentObject.transform);
            setUsedObject(spawnedObject, itemCode);
        }
    }
}
