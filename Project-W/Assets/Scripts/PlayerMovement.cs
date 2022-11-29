using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask placeableMask;     //groundMask
    [SerializeField]
    float walkingSpeed;
    [SerializeField]
    float runningSpeed;
    float velocity;
    [SerializeField]
    float gravity;
    [SerializeField]
    float minimumVelocity;
    [SerializeField]
    float jumpForce;
    bool grounded;

    void Update()
    {
        movement();
        verticalMovement();
        jump();
    }

    void movement()
    {
        switch (FindObjectOfType<Player>().getMovementState())
        {
            case "WALKING":
                walkingState();
                break;
            case "RUNNING":
                runningState();
                break;
        }

   /*     if (Input.GetKey(KeyCode.Z))
        {
            transform.position = transform.position + new Vector3(0, 5 * Time.deltaTime, 0);
            actualGravity = 0;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
            transform.position = transform.position + new Vector3(0, -2 * Time.deltaTime, 0);*/
    }

    void walkingState()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        controller.Move(move * walkingSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            FindObjectOfType<Player>().setState("RUNNING");
    }

    void runningState()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        controller.Move(move * walkingSpeed * Time.deltaTime);     

        if (!Input.GetKey(KeyCode.LeftShift))
            FindObjectOfType<Player>().setState("WALKING");
    }

    void verticalMovement()
    {
        velocity += gravity * Time.deltaTime;
        if (velocity < minimumVelocity)
            velocity = minimumVelocity;

        Vector3 fall = transform.up * velocity;
        controller.Move(fall * Time.deltaTime);
    }

    void jump()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.2f, placeableMask);

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
            velocity = jumpForce;
    }
}
