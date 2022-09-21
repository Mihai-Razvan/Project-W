using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour
{
    [SerializeField]
    GameObject boxPrefab;
    GraphicRaycaster myRaycaster;
    PointerEventData myPointerEventData;
    EventSystem myEventSystem;

    private void Start()
    {
        myRaycaster = GetComponent<GraphicRaycaster>();
        myEventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if(FindObjectOfType<Inventory_Exchange>().getState().Equals("ACTIVE"))    //it means the frag already occured
        {
            myPointerEventData = new PointerEventData(myEventSystem);
            myPointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            myRaycaster.Raycast(myPointerEventData, results);

            if(results.Count == 0)
            {
                int slot = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getSlotNumber();
                int itemCode = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getItemCode(slot);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {    
                    int quantity = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(slot);
                    dropBox(slot, itemCode, quantity);
                }
                else if(Input.GetKeyUp(KeyCode.Mouse1))
                    dropBox(slot, itemCode, 1);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))  
                FindObjectOfType<Inventory_Exchange>().dragEnd(0, 0, null);
        }
    }

    void dropBox(int slot, int itemCode, int quantity)
    {
        Vector2 pos = FindObjectOfType<Player>().getPlayerTransform().position + FindObjectOfType<Player>().getPlayerTransform().forward.normalized * 2;
        GameObject box = Instantiate(boxPrefab, pos, Quaternion.identity); 
        box.gameObject.GetComponent<Item_003>().setBox(itemCode, quantity);

        int initialQuantity = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(slot);
        FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(slot, itemCode, initialQuantity- quantity);
        FindObjectOfType<Inventory_Exchange>().disableItemImage();
    }

}
