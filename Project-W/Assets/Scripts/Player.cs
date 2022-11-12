using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string movementState;     //how the player is: WALKING, IDLE, RUNNING
    static string actionLock;   //what actions are locked: ACTION_LOCK (can't open invenotry, settings etc but can move), "MOVE_LOCK"(or smth) can't moce but can open invenotry
    GameObject groundCheck;   //not used yet, it will be used to check if player is grounded; it is not used for keeping the player on the ground

    void Start()
    {
        movementState = "WALKING";
        actionLock = "UNLOCKED";
     //   groundCheck = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        FindObjectOfType<PlayerMovement>().movement(movementState, transform);
        FindObjectOfType<CameraLook>().rotateCamera(transform);
    }

    public Vector3 getPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void setState(string state)
    {
        this.movementState = state;
    }

    public string getMovementState()
    {
        return movementState;
    }

    public static void setActionLock(string actionLock)
    {
        Player.actionLock = actionLock;
    }

    public static string getActionLock()
    {
        return actionLock;
    }

    public Transform getPlayerTransform()
    {
        return transform;
    }
}
