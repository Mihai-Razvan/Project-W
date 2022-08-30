using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected int itemCode;

    static GameObject usedObject;           //the object that is currently used (ex tools, weapons, building you are placing); is null if it is an object you won't have in hands
                                            //when it is selected from inventory (ex resources)
    protected bool checkSelected()
    {
        return FindObjectOfType<Inventory>().getSelectedItem() == itemCode;
    }

    public static int getItemCode(string itemTag)
    {
        switch (itemTag)
        {
            case "Item_001":
                return 1;
            default:
                return 0;
        }
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
