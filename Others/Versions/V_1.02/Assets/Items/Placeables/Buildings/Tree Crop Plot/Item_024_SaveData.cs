using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_024_SaveData : PlaceableData
{
    public string status;
    public int index;
    public int fruitItemCode;
    public float timeSincePlanted;

    public Item_024_SaveData(ArrayList data)
    {
        this.status = (string)data[0];
        this.index = (int)data[1];
        this.fruitItemCode = (int)data[2];
        this.timeSincePlanted = (float)data[3];
    }

    public static List<Item_024_SaveData> getItemsData()
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<Item_024_SaveData> item_024_Data = new List<Item_024_SaveData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i] is BoxCollider && ItemsList.getItemCode(colliders[i].gameObject.tag) == 24)  //we check to be a boxCollider in case that object got other colliders to, otherwise the object would be saved once for every collider
            {
                item_024_Data.Add(new Item_024_SaveData(colliders[i].gameObject.GetComponent<Item_024>().getSaveData()));
                item_024_Data[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                item_024_Data[k].position[0] = colliders[i].gameObject.transform.position.x;
                item_024_Data[k].position[1] = colliders[i].gameObject.transform.position.y;
                item_024_Data[k].position[2] = colliders[i].gameObject.transform.position.z;

                item_024_Data[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                item_024_Data[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                item_024_Data[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                k++;
            }

        return item_024_Data;
    }
}
