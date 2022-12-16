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

    void Awake()  //we use awake because if we use start it is called before load from the save file
    {
        waterCylinder.SetActive(false);
        cylinderAnim.enabled = false;
        smallWater.SetActive(false);
        handleBigWaterCube();
    }

    void Update()
    {
        if (Interactions.getInRangeBuilding() == this.gameObject && ActionLock.getActionLock().Equals("UNLOCKED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }

            buttonHintHandle();
        }

        if (batteryPlaced == true && batteryCharge > 0 && filled == false)
            produceWater();
    }

    void interaction()
    {
        if (selectedItemCode == 17)       //battery 
            placeBattery();
        else if (selectedItemCode == 22 && filled == true)    //empty can
        {
            int playerInventorySlot = Player_Inventory.getSelectedSlot();
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().setSlot(playerInventorySlot, 23, 1, 0);   //replace the empty cup with a full cup
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
            int playerInventorySlot = Player_Inventory.getSelectedSlot();
            batteryCharge = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getCharge(playerInventorySlot);
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
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
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(17, 1, batteryCharge);
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
        float yScale = Mathf.Min(fillAmount * maxWaterCubeScale / 100, maxWaterCubeScale);      //in case fill time gets reduced, when we load then the cube would be bigger that maxScale
        bigWater.transform.localScale = new Vector3(bigWater.transform.localScale.x, yScale, bigWater.transform.localScale.z);
    }

    void buttonHintHandle()
    {
        if (selectedItemCode == 17)
        {
            if (batteryPlaced == false)
                Button_Hint.setBuildingInteractionHint("Place 'Battery'");
            else
                Button_Hint.setBuildingInteractionHint("Collect 'Battery'");
        }
        else if (selectedItemCode == 22 && filled == true)
            Button_Hint.setBuildingInteractionHint("Fill 'Empty Cup'");
        else if (batteryPlaced == true)
            Button_Hint.setBuildingInteractionHint("Collect 'Battery'");
        else
            Button_Hint.clearBuildingInteractionHint();
    }



    public ArrayList getSaveData()
    {
        return new ArrayList() { batteryPlaced, batteryCharge, filled, timeSinceStarted };
    }

    public void loadData(bool batteryPlaced, float batteryCharge, bool filled, float timeSinceStarted)     //used when we are loading this object from file
    {
        this.batteryPlaced = batteryPlaced;
        this.batteryCharge = batteryCharge;
        this.filled = filled;
        this.timeSinceStarted = timeSinceStarted;

        if(batteryPlaced == true)
        {
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
            spawnedModel.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);

            if(batteryCharge > 0)
            {
                waterCylinder.SetActive(true);
                cylinderAnim.enabled = true;
            }
        }


        if (filled == true)
            smallWater.SetActive(true);

        handleBigWaterCube();
    }
}
