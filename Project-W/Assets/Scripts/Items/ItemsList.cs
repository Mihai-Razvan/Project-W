using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField]
    Sprite[] spriteArray;
    [SerializeField]
    string[] nameArray;

    public Sprite getSprite(int itemCode)
    {
        return spriteArray[itemCode];
    }

    public string getName(int itemCode)
    {
        return nameArray[itemCode];
    }
}
