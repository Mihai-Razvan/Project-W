using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_028_SaveData : PlaceableData
{
    public int[] itemCodeArray;
    public int[] quantityArray;
    public float[] chargeArray;

    public Item_028_SaveData(ArrayList data)
    {
        this.itemCodeArray = (int[])data[0];
        this.quantityArray = (int[])data[1];
        this.chargeArray = (float[])data[2];
    }

    public static List<Item_028_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_028_SaveData> item_028_Data = new List<Item_028_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (ItemsList.getItemCode(colliders[i].gameObject.tag) == 28)
            {
                item_028_Data.Add(new Item_028_SaveData(colliders[i].gameObject.GetComponent<Item_028>().getSaveData()));
                item_028_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_028_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_028_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_028_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_028_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_028_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_028_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_028_Data;
    }
}
