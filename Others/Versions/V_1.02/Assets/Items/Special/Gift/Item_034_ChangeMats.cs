using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_034_ChangeMats : MonoBehaviour
{
    [SerializeField]
    Material[] possbileColors;
    [SerializeField]
    Material commonMaterial;
    void Start()
    {
        int randVal = Random.Range(0, possbileColors.Length);
        Material[] mats = new Material[] { possbileColors[randVal], commonMaterial };
        GetComponent<Renderer>().materials = mats;

        Destroy(this.gameObject.GetComponent<Gift_Change_Colors>());
    }
}
