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

            if (results.Count == 0 && (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1)))
                dropBox();
        }
    }

    void dropBox()
    {
        Vector2 pos = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x * 5, 3, Camera.main.transform.forward.z * 5);
        Instantiate(boxPrefab, pos, Quaternion.Euler(0, 0, 0));

        int slot = FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getSlotNumber();
        FindObjectOfType<Inventory_Exchange>().getDragSlotObject().GetComponent<Inventory_Slot>().getInventoryHolder().GetComponent<Inventory>().setSlot(slot, 0, 0);
        FindObjectOfType<Inventory_Exchange>().disableItemImage();
    }

}
