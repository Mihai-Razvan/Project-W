using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int itemCode;     

    static GameObject usedObject;           //the object that is currently used (ex tools, weapons, building you are placing); is null if it is an object you won't have in hands
                                            //when it is selected from inventory (ex resource)
    protected bool checkSelected()
    {
        return FindObjectOfType<Player_Inventory>().getSelectedItem() == itemCode;
    }

    public static int getItemCode(string itemTag)
    {
        int itemCode = itemTag[5] - '0';
        itemCode = itemCode * 10 + itemTag[6] - '0';
        itemCode = itemCode * 10 + itemTag[7] - '0';

        return itemCode;
    }

    public static void destroyUsedObject()   //destroys the prefab of the object you have in your hands
    {
        if (usedObject != null)
            Destroy(usedObject);
    }

    public static void setUsedObject(GameObject usedObject)
    {
        Item.usedObject = usedObject;
    }

    public static GameObject getUsedObject()
    {
        return usedObject;
    }
}
