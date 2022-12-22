using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]
    GameObject centerObject;
    [SerializeField]
    float rotationSpeed;
    
    void Update()
    {
        transform.RotateAround(centerObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
