using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_016 : MonoBehaviour
{
    [SerializeField]
    Animator bigCircleAnimator;
    [SerializeField]
    Animator smallCircleAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        rotateBlades();
    }

    void rotateBlades()
    {
        float animationSpeed = FindObjectOfType<Wind>().getSpeed() / 10;
        bigCircleAnimator.speed = animationSpeed;
        smallCircleAnimator.speed = animationSpeed * 3;
    }
}
