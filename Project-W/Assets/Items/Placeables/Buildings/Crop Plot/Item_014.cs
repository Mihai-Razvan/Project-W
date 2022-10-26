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
        if(FindObjectOfType<Interactions>().getInRangeBuilding() == this.gameObject && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
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
                        int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
                        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
                        return;
                    }
    }

    void collectCrop(int plantingSlot)
    {
        FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(cropItemCodeArray[plantingSlot], 2);
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
        GameObject spawnedModel = Instantiate(modelStage1[getModelIndex(plantingSlot)], plantingSpots[plantingSlot].transform.position, Quaternion.identity);
        spawnedModel.transform.SetParent(plantingSpots[plantingSlot].transform);
    }

    void grow()
    {
        for (int i = 0; i < 3; i++)
        {
            timeArray[i] += Time.deltaTime;
            if (slotStatusArray[i].Equals("STAGE_1") && timeArray[i] > FindObjectOfType<Food_Data>().getGrowingTime(cropItemCodeArray[i]))
            {
                slotStatusArray[i] = "STAGE_2";
                Destroy(plantingSpots[i].transform.GetChild(1).gameObject);
                GameObject spawnedModel = Instantiate(modelStage2[getModelIndex(i)], plantingSpots[i].transform.position, Quaternion.identity);
                spawnedModel.transform.SetParent(plantingSpots[i].transform);
            }
            else if (slotStatusArray[i].Equals("STAGE_2") && timeArray[i] > FindObjectOfType<Food_Data>().getGrowingTime(cropItemCodeArray[i]) * 2)
            {
                slotStatusArray[i] = "STAGE_3";
                Destroy(plantingSpots[i].transform.GetChild(1).gameObject);
                GameObject spawnedModel = Instantiate(modelStage3[getModelIndex(i)], plantingSpots[i].transform.position, Quaternion.identity);
                spawnedModel.transform.SetParent(plantingSpots[i].transform);
            }
        }
    }

    int getModelIndex(int plantingSlot)
    {
        for (int i = 0; i < plantableItemCodeArray.Length; i++)
            if (plantableItemCodeArray[i] == cropItemCodeArray[plantingSlot])
                return i;

        return -1;
    }
}