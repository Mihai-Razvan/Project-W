using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable_Data : MonoBehaviour
{
    [SerializeField]
    string structureType;    //foundation type (foundation, floor; prefab on which you can place walls), wall (different types of walls) !prefab type can be the same as item name but aren't the same thing

    public string getStructureType()
    {
        return structureType;
    }
}
