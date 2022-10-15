using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour   
{
    //This script is placed on Canvas 

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
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 3;
        GameObject box = Instantiate(boxPrefab, pos, Quaternion.identity);
        int[] itemCodeArray = new int[] { itemCode };
        int[] quantityArray = new int[] { quantity };
        box.gameObject.GetComponent<ResourcesData>().setResourceData(itemCodeArray, quantityArray);

        int initialQuantity = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().getQuantity(slot);
        FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(slot, itemCode, initialQuantity- quantity);
        FindObjectOfType<Inventory_Exchange>().disableItemImage();
    }

}
