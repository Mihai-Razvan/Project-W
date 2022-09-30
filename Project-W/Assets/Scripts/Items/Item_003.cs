using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_003 : Item    //dropped box
{
    int holdedItemCode;
    int holdedQuantity;
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 120)   //box despawns after 2 min
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)          //Building layer
        {
            Destroy(GetComponent<Rigidbody>());
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    public void setBox(int itemCode, int quantity)
    {
        this.holdedItemCode = itemCode;
        this.holdedQuantity = quantity;
    }

    public int getItemCode()
    {
        return holdedItemCode;
    }

    public int getQuantity()
    {
        return holdedQuantity;
    }
}
