using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_021_SaveData : PlaceableData
{
    public bool batteryPlaced;
    public float batteryCharge;
    public bool filled;
    public float timeSinceStarted;

    public Item_021_SaveData(ArrayList data)
    {
        this.batteryPlaced = (bool)data[0];
        this.batteryCharge = (float)data[1];
        this.filled = (bool)data[2];
        this.timeSinceStarted = (float)data[3];
    }
    public static List<Item_021_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_021_SaveData> item_021_Data = new List<Item_021_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i] is BoxCollider && ItemsList.getItemCode(colliders[i].gameObject.tag) == 21)  //we check to be a boxCollider in case that object got other colliders to, otherwise the object would be saved once for every collider
            {
                item_021_Data.Add(new Item_021_SaveData(colliders[i].gameObject.GetComponent<Item_021>().getSaveData()));
                item_021_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_021_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_021_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_021_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_021_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_021_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_021_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_021_Data;
    }
}
