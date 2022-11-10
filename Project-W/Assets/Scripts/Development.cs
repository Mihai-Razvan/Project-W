using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Development : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    GameObject player;

    void Update()
    {
       // if (Input.GetKey(KeyCode.Q))
      //      Application.Quit();

        if (Input.GetKeyDown(KeyCode.H))
            canvas.GetComponent<Canvas>().enabled = !canvas.GetComponent<Canvas>().enabled;

    //    if (Input.GetKeyDown(KeyCode.F))
     //       controller.enabled = !controller.enabled;

        if (Input.GetKeyDown(KeyCode.T))
            player.transform.position = new Vector3(0, 120, 0);
    }
}
