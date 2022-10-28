using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float speed;
    float timeSinceChanged;
    public delegate void OnWindSpeedChange();
    public OnWindSpeedChange onWindSpeedChange;

    void Awake()
    {
        speed = 10;
        onWindSpeedChange += f;     //delegates requires a function at least; otherwise nullpointerexception
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
            onWindSpeedChange();
        }
    }

    public float getSpeed()
    {
        return speed;
    }

    void f()
    {
        
    }
}
