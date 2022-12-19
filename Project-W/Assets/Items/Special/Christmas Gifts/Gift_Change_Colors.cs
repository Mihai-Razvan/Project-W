using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift_Change_Colors : MonoBehaviour
{
    [SerializeField]
    Material[] possbileColors;
    [SerializeField]
    Material commonMaterial;
    void Start()
    {
        GameObject gift = transform.GetChild(0).gameObject;
        int randVal = Random.Range(0, possbileColors.Length);
        Material[] mats = new Material[] { possbileColors[randVal], commonMaterial };
        gift.GetComponent<Renderer>().materials = mats;

        Destroy(this.gameObject.GetComponent<Gift_Change_Colors>());
    }
}
