using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceableData           //placeable data for the save system
{
    public int itemCode;
    public float[] position;
    public float[] rotation;

    public PlaceableData()
    {
        position = new float[3];
        rotation = new float[3];
    }

    public static List<PlaceableData> getPlaceables(List<int> itemCodes)        //the itemCodes of the placeables to save
    {
        LayerMask PlaceablesMask = LayerMask.GetMask("Placeable");
        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 0, 0), 10000, PlaceablesMask);
        List<PlaceableData> placeableData = new List<PlaceableData>();
        int k = 0;

        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i] is BoxCollider && itemCodes.Contains(ItemsList.getItemCode(colliders[i].gameObject.tag)))  //we check to be a boxCollider in case that object got other colliders to, otherwise the object would be saved once for every collider
                {
                    placeableData.Add(new PlaceableData());
                    placeableData[k].itemCode = ItemsList.getItemCode(colliders[i].gameObject.tag);

                    placeableData[k].position[0] = colliders[i].gameObject.transform.position.x;
                    placeableData[k].position[1] = colliders[i].gameObject.transform.position.y;
                    placeableData[k].position[2] = colliders[i].gameObject.transform.position.z;

                    placeableData[k].rotation[0] = colliders[i].gameObject.transform.eulerAngles.x;
                    placeableData[k].rotation[1] = colliders[i].gameObject.transform.eulerAngles.y;
                    placeableData[k].rotation[2] = colliders[i].gameObject.transform.eulerAngles.z;

                    k++;
                }

        return placeableData;
    }
}
