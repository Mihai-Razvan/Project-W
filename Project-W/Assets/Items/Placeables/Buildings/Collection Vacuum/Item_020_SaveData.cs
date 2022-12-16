using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_020_SaveData : PlaceableData
{
    public List<int> collectedResourceCodes;
    public List<int[]> resourceItemCodeArray;
    public List<int[]> resourceQuantityArray;
    public List<float[]> resourceChargeArray;

    public Item_020_SaveData(ArrayList data)
    {
        this.collectedResourceCodes = (List<int>)data[0];
        this.resourceItemCodeArray = (List<int[]>)data[1];
        this.resourceQuantityArray = (List<int[]>)data[2];
        this.resourceChargeArray = (List<float[]>)data[3];
    }

    public static List<Item_020_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_020_SaveData> item_020_Data = new List<Item_020_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (ItemsList.getItemCode(colliders[i].gameObject.tag) == 20)
            {
                item_020_Data.Add(new Item_020_SaveData(colliders[i].gameObject.GetComponent<Item_020>().getSaveData()));
                item_020_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_020_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_020_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_020_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_020_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_020_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_020_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_020_Data;
    }
}
