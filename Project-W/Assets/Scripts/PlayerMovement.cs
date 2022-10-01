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
        switch (FindObjectOfType<Player>().getMovementState())
        {
            case "WALKING":
                walkingState(playerTransform);
                break;
            case "RUNNING":
                runningState(playerTransform);
                break;
        }

        if (Input.GetKey(KeyCode.LeftControl))
            transform.position = transform.position + new Vector3(0, 5 * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.LeftAlt))
            transform.position = transform.position + new Vector3(0, -2 * Time.deltaTime, 0);
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
