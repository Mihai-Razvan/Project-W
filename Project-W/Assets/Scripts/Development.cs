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

    private void Start()
    {
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        // if (Input.GetKey(KeyCode.Q))
        //      Application.Quit();

        if (Input.GetKeyDown(KeyCode.H))
            canvas.GetComponent<Canvas>().enabled = !canvas.GetComponent<Canvas>().enabled;
            

        if(Input.GetKeyDown(KeyCode.C))
            UnityEngine.Cursor.visible = !UnityEngine.Cursor.visible;

        //    if (Input.GetKeyDown(KeyCode.F))
        //       controller.enabled = !controller.enabled;

        if (Input.GetKeyDown(KeyCode.T))
            player.transform.position = new Vector3(0, 120, 0);

        if (Input.GetKeyDown(KeyCode.B))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(1, 1, 0);

        if (Input.GetKeyDown(KeyCode.N))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(9, 1, 0);

        if (Input.GetKeyDown(KeyCode.M))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(11, 1, 0);

      
    }
}
