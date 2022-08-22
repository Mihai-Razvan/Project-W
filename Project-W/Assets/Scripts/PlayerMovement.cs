using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed;
    [SerializeField]
    float runningSpeed;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float gravity;

    public void movement(string state, Transform playerTransform)
    {
        switch (FindObjectOfType<Player>().getState())
        {
            case "WALKING":
                walkingState(playerTransform);
                break;
            case "RUNNING":
                runningState(playerTransform);
                break;
        }
    }

    void walkingState(Transform playerTransform)
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 move = playerTransform.right * horizontalMovement + playerTransform.forward * verticalMovement + playerTransform.up * gravity * Time.deltaTime;
        controller.Move(move * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            FindObjectOfType<Player>().setState("RUNNING");
    }

    void runningState(Transform playerTransform)
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 move = playerTransform.right * horizontalMovement + playerTransform.forward * verticalMovement + playerTransform.up * gravity * Time.deltaTime; 
        controller.Move(move * runningSpeed * Time.deltaTime);

        if (!Input.GetKey(KeyCode.LeftShift))
            FindObjectOfType<Player>().setState("WALKING");
    }
}
