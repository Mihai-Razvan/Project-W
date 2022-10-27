using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float speed;
    float timeSinceChanged;

    private void Start()
    {
        speed = 10;
    }

    void Update()
    {
        changeSpeed();
    }

    void changeSpeed()
    {
        timeSinceChanged += Time.deltaTime;

        if (timeSinceChanged >= 10)
        {
            speed = Random.Range(10, 60);
            timeSinceChanged = 0;
        }
    }

    public float getSpeed()
    {
        return speed;
    }
}
