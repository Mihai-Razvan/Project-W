using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_014_SaveData : PlaceableData
{
    public int[] cropItemCodeArray;
    public string[] slotStatusArray;
    public float[] timeArray;

    public Item_014_SaveData(ArrayList data)
    {
        this.cropItemCodeArray = (int[]) data[0];
        this.slotStatusArray = (string[]) data[1];
        this.timeArray = (float[]) data[2];
    }

    public static List<Item_014_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_014_SaveData> item_014_Data = new List<Item_014_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (ItemsList.getItemCode(colliders[i].gameObject.tag) == 14)
            {
                item_014_Data.Add(new Item_014_SaveData(colliders[i].gameObject.GetComponent<Item_014>().getSaveData()));
                item_014_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_014_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_014_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_014_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_014_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_014_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_014_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_014_Data;
    }
}
