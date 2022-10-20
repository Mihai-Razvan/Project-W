using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Data : MonoBehaviour
{
    [SerializeField]
    int[] itemCodeArray;
    [SerializeField]
    float[] cropGrowingTime;     //THE TIME IS PER GROWING STAGE, NOT TOTAL GROWING TIME; if it s something you can plant, time will be different than -1

    public float getGrowingTime(int itemCode)
    {
        for (int i = 0; i < itemCodeArray.Length; i++)
            if (itemCodeArray[i] == itemCode)
                return cropGrowingTime[i];

        return -1;
    }
}
