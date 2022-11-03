using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_024 : MonoBehaviour
{
    [SerializeField]
    GameObject treePlace;
    [SerializeField]
    int[] plantableItemCodeArray;   //item codes for crops you can plant in this tree crop plot
    [SerializeField]
    GameObject dirtHolePlace;
    string status;
    int fruitItemCode;
    int fruitsLeft;

    void Start()
    {
        status = "EMPTY";
    }

    void Update()
    {
        if (FindObjectOfType<Interactions>().getInRangeBuilding() == this.gameObject && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }
        }

      //  grow();
    }

    void interaction()
    {
       if(status.Equals("EMPTY"))
        {
            collectFruits();
            return;
        }
    }

    void collectFruits()
    {
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(fruitItemCode, 2);
        fruitsLeft--;       
        Destroy(dirtHolePlace.transform.GetChild(0).gameObject);  //the plant
        Destroy(this.gameObject.transform.GetChild(0).GetChild(0).gameObject);  //the dirt hole
    }

    int getIndex(int itemCode)
    {
        for (int i = 0; i < plantableItemCodeArray.Length; i++)
            if (plantableItemCodeArray[i] == itemCode)
                return i;

        return -1;
    }
}
