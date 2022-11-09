using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_021 : Item   //Atmosphere Condensator
{
    [SerializeField]
    GameObject batteryHole;
    [SerializeField]
    GameObject batteryPrefab;
    [SerializeField]
    GameObject bigWater;
    [SerializeField]
    GameObject smallWater;
    [SerializeField]
    GameObject waterCylinder;
    [SerializeField]
    Animator cylinderAnim;
    [SerializeField]
    float batteryConsumption;
    [SerializeField]
    int fillTime;
    [SerializeField]
    float maxWaterCubeScale;
    bool batteryPlaced;
    float batteryCharge;
    bool filled;
    float timeSinceStarted;
    
    void Start()
    {
        waterCylinder.SetActive(false);
        cylinderAnim.enabled = false;
        smallWater.SetActive(false);
        handleBigWaterCube();
    }

    void Update()
    {
        if (FindObjectOfType<Interactions>().getInRangeBuilding() == this.gameObject && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }
        }

        if (batteryPlaced == true && batteryCharge > 0 && filled == false)
            produceWater();
    }

    void interaction()
    {
        if (FindObjectOfType<Player_Inventory>().getSelectedItemCode() == 17)       //battery 
            placeBattery();
        else if (FindObjectOfType<Player_Inventory>().getSelectedItemCode() == 22 && filled == true)    //empty can
        {
            int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().setSlot(playerInventorySlot, 23, 1, 0);   //replace the empty cup with a full cup
            filled = false;
            timeSinceStarted = 0;
            handleBigWaterCube();
            smallWater.SetActive(false);
        }
        else if (batteryPlaced == true)
            collectBattery();
    }

    void placeBattery()
    {
        if (batteryPlaced == false)
        {
            int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
            batteryCharge = FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().getCharge(playerInventorySlot);
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
            spawnedModel.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            batteryPlaced = true;
            if (batteryCharge <= 0)
            {
                waterCylinder.SetActive(false);
                cylinderAnim.enabled = false;
            }
            else
            {
                waterCylinder.SetActive(true);
                cylinderAnim.enabled = true;
            }
        }
        else
            collectBattery();   //in case you have a battery placed and in the same time a battery in hand
    }

    void collectBattery()
    {
        if (batteryPlaced == true)
        {
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(17, 1, batteryCharge);
            Destroy(batteryHole.transform.GetChild(0).gameObject);
            batteryPlaced = false;
            waterCylinder.SetActive(false);
            cylinderAnim.enabled = false;
        }
    }

    void produceWater()
    {
        timeSinceStarted += Time.deltaTime;
        batteryCharge -= batteryConsumption * Time.deltaTime;
        if(batteryCharge <= 0)
        {
            waterCylinder.SetActive(false);
            cylinderAnim.enabled = false;
        }

        if (timeSinceStarted > fillTime)
        {
            filled = true;
            smallWater.SetActive(true);
        }

        handleBigWaterCube();
    }

    void handleBigWaterCube()
    {
        float fillAmount = (timeSinceStarted / fillTime) * 100;
        float yScale = (fillAmount * maxWaterCubeScale) / 100;
        bigWater.transform.localScale = new Vector3(bigWater.transform.localScale.x, yScale, bigWater.transform.localScale.z);
    }
}
