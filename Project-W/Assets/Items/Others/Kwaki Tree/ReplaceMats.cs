using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceMats : MonoBehaviour
{
    [SerializeField]
    Material[] dryMats;
    [SerializeField]
    Material[] disolveMats;

    public void replaceDissolving()
    {
        GameObject tree = transform.GetChild(0).gameObject;
        Material[] trunkMats = new Material[] { disolveMats[0], disolveMats[1] };
        tree.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = trunkMats;

        for (int i = 1; i < tree.transform.childCount; i++)
        {
            tree.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = disolveMats[i + 1];
        }
    }

    public void replaceDry()
    {
        GameObject tree = transform.GetChild(0).gameObject;
        Material[] trunkMats = new Material[] { dryMats[0], dryMats[1] };
        tree.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = trunkMats;

        for (int i = 1; i < tree.transform.childCount; i++)
        {
            tree.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = dryMats[i + 1];
        }
    }
}
