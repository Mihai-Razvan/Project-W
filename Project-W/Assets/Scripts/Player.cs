using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string state;

    void Start()
    {
        state = "WALKING";
    }

    void Update()
    {
        FindObjectOfType<PlayerMovement>().movement(state, transform);
        FindObjectOfType<CameraLook>().rotateCamera(transform);
    }

    public Vector3 getPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void setState(string state)
    {
        this.state = state;
    }

    public string getState()
    {
        return state;
    }
}
