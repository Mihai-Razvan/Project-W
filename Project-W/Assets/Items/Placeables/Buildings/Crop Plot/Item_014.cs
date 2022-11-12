using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_014 : Item          //crop plot
{
    [SerializeField]
    GameObject dirtHolePrefab;
    [SerializeField]
    GameObject[] plantingSpots;
    int[] cropItemCodeArray;
    string[] slotStatusArray;     
    float[] timeArray;
    [SerializeField]
    int[] plantableItemCodeArray;   //item codes for crops you can plant in this crop plot
    [SerializeField]
    int[] growingTime;              //this is the growing time per stage; so multiply this with 2 for the total grow time
    [SerializeField]
    GameObject[] modelStage1;
    [SerializeField]
    GameObject[] modelStage2;
    [SerializeField]
    GameObject[] modelStage3;

    void Start()
    {
        cropItemCodeArray = new int[3];
        slotStatusArray = new string[] { "EMPTY", "EMPTY", "EMPTY" };
        timeArray = new float[3];
    }

    void Update()
    {
        if(Interactions.getInRangeBuilding() == this.gameObject && Player.getActionLock().Equals("INVENTORY_OPENED") == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }
        }

        grow();
    }

    void interaction()
    {
        for (int i = 0; i < 3; i++)       //collects grown crops
            if (slotStatusArray[i].Equals("STAGE_3"))
            {
                collectCrop(i);
                return;
            }

        for (int i = 0; i < 3; i++)        //plants new crops
            if (slotStatusArray[i].Equals("EMPTY"))
                for (int j = 0; j < plantableItemCodeArray.Length; j++)
                    if (Item.getUsedObjectItemCode() == plantableItemCodeArray[j])
                    {
                        plant(i, Item.getUsedObjectItemCode());
                        int playerInventorySlot = Player_Inventory.getSelectedSlot();
                        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
                        return;
                    }
    }

    void collectCrop(int plantingSlot)
    {
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(cropItemCodeArray[plantingSlot], 2, 0);
        cropItemCodeArray[plantingSlot] = 0;
        slotStatusArray[plantingSlot] = "EMPTY";
        Destroy(plantingSpots[plantingSlot].transform.GetChild(0).gameObject);  //the dirt hole
        Destroy(plantingSpots[plantingSlot].transform.GetChild(1).gameObject);  //the plant
    }

    void plant(int plantingSlot, int cropItemCode)
    {
        cropItemCodeArray[plantingSlot] = cropItemCode;
        slotStatusArray[plantingSlot] = "STAGE_1";
        timeArray[plantingSlot] = 0;
        GameObject dirtHole = Instantiate(dirtHolePrefab, plantingSpots[plantingSlot].transform.position, Quaternion.identity);
        dirtHole.transform.SetParent(plantingSpots[plantingSlot].transform);
        GameObject spawnedModel = Instantiate(modelStage1[getIndex(plantingSlot)], plantingSpots[plantingSlot].transform.position, Quaternion.identity);
        spawnedModel.transform.SetParent(plantingSpots[plantingSlot].transform);
    }

    void grow()
    {
        for (int i = 0; i < 3; i++)
        {
            timeArray[i] += Time.deltaTime;
            if (slotStatusArray[i].Equals("STAGE_1") && timeArray[i] > growingTime[getIndex(i)])
            {
                slotStatusArray[i] = "STAGE_2";
                Destroy(plantingSpots[i].transform.GetChild(1).gameObject);
                GameObject spawnedModel = Instantiate(modelStage2[getIndex(i)], plantingSpots[i].transform.position, Quaternion.identity);
                spawnedModel.transform.SetParent(plantingSpots[i].transform);
            }
            else if (slotStatusArray[i].Equals("STAGE_2") && timeArray[i] > growingTime[getIndex(i)] * 2)
            {
                slotStatusArray[i] = "STAGE_3";
                Destroy(plantingSpots[i].transform.GetChild(1).gameObject);
                GameObject spawnedModel = Instantiate(modelStage3[getIndex(i)], plantingSpots[i].transform.position, Quaternion.identity);
                spawnedModel.transform.SetParent(plantingSpots[i].transform);
            }
        }
    }

    int getIndex(int plantingSlot)
    {
        for (int i = 0; i < plantableItemCodeArray.Length; i++)
            if (plantableItemCodeArray[i] == cropItemCodeArray[plantingSlot])
                return i;

        return -1;
    }
}
