using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Data : MonoBehaviour
{
    [SerializeField]
    string prefabType;    //foundation type (foundation, floor; prefab on which you can place walls), wall (different types of walls) !prefab type can be the same as item name but aren't the same thing

    public string getPrefabType()
    {
        return prefabType;
    }
}
