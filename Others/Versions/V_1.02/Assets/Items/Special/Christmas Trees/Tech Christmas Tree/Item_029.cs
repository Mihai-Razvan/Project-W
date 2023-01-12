using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_029 : MonoBehaviour
{
    [SerializeField]
    Material[] mats;
    [SerializeField]
    float changeInterval;
    float timeSinceColorReached;
    int actualColor;

    void Start()
    {
        timeSinceColorReached = 0;
        actualColor = 0;
    }

    void Update()
    {
        timeSinceColorReached += Time.deltaTime;

        if(timeSinceColorReached >= changeInterval)
        {
            timeSinceColorReached = 0;

            actualColor++;
            if (actualColor == mats.Length) 
                actualColor = 0;
        }

        GameObject tree = transform.GetChild(0).gameObject;
        Material[] newMats = tree.GetComponent<Renderer>().materials;
        float lerpVal = timeSinceColorReached / changeInterval;

        if(actualColor + 1 < mats.Length)
            newMats[1].Lerp(mats[actualColor], mats[actualColor + 1], lerpVal);
        else
            newMats[1].Lerp(mats[actualColor], mats[0], lerpVal);

        tree.GetComponent<Renderer>().materials = newMats;
    }
}
