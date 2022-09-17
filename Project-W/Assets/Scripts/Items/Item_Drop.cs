using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1) && FindObjectOfType<Inventory_Exchange>().getState().Equals("ACTIVE"))
        {

        }
    }

}
