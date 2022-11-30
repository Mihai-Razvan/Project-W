using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField]
    Transform playerTransformHelper;
    [SerializeField]
    Transform cameraTransformHelper;

    static Transform playerTransform;
    static Transform cameraTransform;
    static float rotationSpeed;
    static float xRotation;

    void Start()
    {
        xRotation = 0;
        rotationSpeed = 150;
        playerTransform = playerTransformHelper;
        cameraTransform = cameraTransformHelper;
    }

    public static void rotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
