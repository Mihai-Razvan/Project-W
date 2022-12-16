using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_019_SaveData : PlaceableData
{
    public int placedFoodItemCode;
    public bool batteryPlaced;
    public bool foodPlaced;
    public float batteryCharge;
    public float timeOnGrill;
    public bool cooked;

    public Item_019_SaveData(ArrayList data)
    {
        this.placedFoodItemCode = (int)data[0];
        this.batteryPlaced = (bool)data[1];
        this.foodPlaced = (bool)data[2];
        this.batteryCharge = (float)data[3];
        this.timeOnGrill = (float)data[4];
        this.cooked = (bool)data[5];
    }

    public static List<Item_019_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_019_SaveData> item_019_Data = new List<Item_019_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (ItemsList.getItemCode(colliders[i].gameObject.tag) == 19)
            {
                item_019_Data.Add(new Item_019_SaveData(colliders[i].gameObject.GetComponent<Item_019>().getSaveData()));
                item_019_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_019_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_019_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_019_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_019_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_019_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_019_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_019_Data;
    }
}
