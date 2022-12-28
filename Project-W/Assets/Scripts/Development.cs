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
       
        if (Input.GetKeyDown(KeyCode.H))
            canvas.GetComponent<Canvas>().enabled = !canvas.GetComponent<Canvas>().enabled;

        development();
    }

    void development()
    {
      if(Input.GetKeyDown(KeyCode.C))
            UnityEngine.Cursor.visible = !UnityEngine.Cursor.visible;

        //    if (Input.GetKeyDown(KeyCode.F))
        //       controller.enabled = !controller.enabled;

        if (Input.GetKeyDown(KeyCode.T))
            FindObjectOfType<PlayerMovement>().setPlayerPosition(new Vector3(0, 120, 0));

        if (Input.GetKeyDown(KeyCode.B))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(1, 1, 0);

        if (Input.GetKeyDown(KeyCode.N))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(9, 1, 0);

        if (Input.GetKeyDown(KeyCode.M))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(11, 1, 0);

        if (Input.GetKeyDown(KeyCode.V))
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(33, 1, 0);
    }
}
