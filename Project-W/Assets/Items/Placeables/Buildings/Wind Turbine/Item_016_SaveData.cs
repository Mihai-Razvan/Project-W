using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_016_SaveData : PlaceableData
{
    public bool batteryPlaced;
    public float batteryCharge;
    public float timeSinceCharged;

    public Item_016_SaveData(ArrayList data)
    {
        this.batteryPlaced = (bool) data[0];
        this.batteryCharge = (float) data[1];
        this.timeSinceCharged = (float)data[2];
    }

    public static List<Item_016_SaveData> getItemsData()        
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_016_SaveData> item_016_Data = new List<Item_016_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (ItemsList.getItemCode(colliders[i].gameObject.tag) == 16)
            {
                item_016_Data.Add(new Item_016_SaveData(colliders[i].gameObject.GetComponent<Item_016>().getSaveData()));
                item_016_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_016_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_016_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_016_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_016_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_016_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_016_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_016_Data;
    }
}
