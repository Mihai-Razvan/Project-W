using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_016 : Item
{
    [SerializeField]
    Animator bigCircleAnimator;
    [SerializeField]
    Animator smallCircleAnimator;
    [SerializeField]
    GameObject batteryHole;
    [SerializeField]
    GameObject batteryPrefab;
    [SerializeField]
    TextMeshProUGUI chargeText;
    [SerializeField]
    TextMeshProUGUI windText;

    bool batteryPlaced;
    [Range(0, 100)]
    float batteryCharge;
    float timeSinceCharged;

    void Awake()     //we use awake because if we use start it is called before load from the save file
    {
        chargeText.text = "-";
        windText.text = "10";
        FindObjectOfType<Wind>().onWindSpeedChange += rotateBlades;
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

        charge();
    }

    void interaction()
    {
        if(batteryPlaced == true)    
        {
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(17, 1, batteryCharge);
            Destroy(batteryHole.transform.GetChild(0).gameObject);
            batteryPlaced = false;
            chargeText.text = "-";
        }
        else if(selectedItemCode == 17)   //battery
        {
            int playerInventorySlot = Player_Inventory.getSelectedSlot();
            batteryCharge = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getCharge(playerInventorySlot);
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
            batteryPlaced = true;
            timeSinceCharged = 0;
            chargeText.text = ((int)batteryCharge).ToString();
        }
    }

    void charge()
    {
        if(batteryPlaced == true && batteryCharge < 100)
        {
            timeSinceCharged += Time.deltaTime;
            if(timeSinceCharged >= 1.2f)
            {
                batteryCharge += 1;
                timeSinceCharged = 0;
                chargeText.text = ((int) batteryCharge).ToString();
            }
        }
    }

    void rotateBlades()
    {
        float animationSpeed = FindObjectOfType<Wind>().getSpeed() / 10;
        bigCircleAnimator.speed = animationSpeed;
        smallCircleAnimator.speed = animationSpeed * 3;
        windText.text = FindObjectOfType<Wind>().getSpeed().ToString();
    }

    void buttonHintHandle()
    {
        if (batteryPlaced == true)
            Button_Hint.setBuildingInteractionHint("Collect 'Battery'");
        else if (selectedItemCode == 17)
            Button_Hint.setBuildingInteractionHint("Place 'Battery'");
        else
            Button_Hint.clearBuildingInteractionHint();
    }

    public ArrayList getSaveData()
    {
        return new ArrayList() { batteryPlaced, batteryCharge, timeSinceCharged };
    }

    public void loadData(bool batteryPlaced, float batteryCharge, float timeSinceCharged)     //used when we are loading this object from file
    {
        this.batteryPlaced = batteryPlaced;
        this.batteryCharge = batteryCharge;
        this.timeSinceCharged = timeSinceCharged;

        if(batteryPlaced == true)
        {
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
        }
    }
}
