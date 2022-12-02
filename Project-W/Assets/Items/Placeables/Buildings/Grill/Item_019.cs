using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_019 : Item   //grill
{
    [SerializeField]
    GameObject batteryHole;
    [SerializeField]
    GameObject foodPrefabPlace;
    [SerializeField]
    GameObject batteryPrefab;
    [SerializeField]
    int[] placeableFood;
    [SerializeField]
    GameObject[] rawFoodsPrefabs;
    [SerializeField]
    GameObject[] cookedFoodsPrefabs;
    [SerializeField]
    int[] grillTime;
    [SerializeField]
    int[] cookedFoodItemCodes;
    [SerializeField]
    float batteryConsumption;
    int placedFoodItemCode;
    bool batteryPlaced;
    bool foodPlaced;
    float batteryCharge;
    float timeOnGrill;
    bool cooked;
    [SerializeField]
    TextMeshProUGUI timeLeftText;
    [SerializeField]
    TextMeshProUGUI battertyChargeText;
    [SerializeField]
    GameObject coalsPlace;
    [SerializeField]
    GameObject coals;
    [SerializeField]
    GameObject fireCoals;

    [SerializeField]
    AudioSource fireSound;
    [SerializeField]
    float maxFireSoundVolume;
   
    void Start()
    {
        timeLeftText.text = "-";
        battertyChargeText.text = "-";

        changeFireSoundVolume(FindObjectOfType<SoundsManager>().getSFxVolume());
        SoundsManager.onSFxVolumeChange += changeFireSoundVolume;
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

        if (foodPlaced == true && cooked == false)
            grill();
    }

    void interaction()
    {
        if(selectedItemCode == 17)       //battery 
            handleBattery();
        else
        {
            if (getIndex(selectedItemCode) != -1 && foodPlaced == false)
                putOnGrill();
            else if (foodPlaced == true && cooked == true)
                collectFood();
            else
                collectBattery();
        }
    }

    void handleBattery()
    {
        if (batteryPlaced == false)
        {
            int playerInventorySlot = Player_Inventory.getSelectedSlot();
            batteryCharge = Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getCharge(playerInventorySlot);
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
            batteryPlaced = true;
            if(batteryCharge <= 0)
                battertyChargeText.text = "EMPTY";
            else
                battertyChargeText.text = ((int)batteryCharge).ToString();
        }
        else
            collectBattery();   //in case you have a battery placed and in the same time a battery in hand

        replaceCoals();
        handleFireSound();
    }

    void collectBattery()
    {
        if(batteryPlaced == true)
        {
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(17, 1, batteryCharge);
            Destroy(batteryHole.transform.GetChild(0).gameObject);
            batteryPlaced = false;
            battertyChargeText.text = "-";
            replaceCoals();
            handleFireSound();
        }
    }

    void putOnGrill()
    {
        GameObject spawnedModel = Instantiate(rawFoodsPrefabs[getIndex(getUsedObjectItemCode())], foodPrefabPlace.transform.position, Quaternion.identity);
        spawnedModel.transform.SetParent(foodPrefabPlace.transform);
        placedFoodItemCode = selectedItemCode;
        int playerInventorySlot = Player_Inventory.getSelectedSlot();
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
        timeOnGrill = 0;
        cooked = false;
        foodPlaced = true;
        int time = grillTime[getIndex(placedFoodItemCode)];
        timeLeftText.text = (time / 60) + ":" + (time % 60);
        replaceCoals();
        handleFireSound();
    }

    void collectFood()
    {
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().addItem(cookedFoodItemCodes[getIndex(placedFoodItemCode)], 1, 0);
        Destroy(foodPrefabPlace.transform.GetChild(0).gameObject);
        foodPlaced = false;
        timeLeftText.text = "-";
    }

    void grill()
    {
        if (batteryCharge <= 0 || batteryPlaced == false)
            return;
    
        timeOnGrill += Time.deltaTime;
        batteryCharge -= batteryConsumption * Time.deltaTime;
        if (batteryCharge <= 0)
        {
            battertyChargeText.text = "EMPTY";
            replaceCoals();
            handleFireSound();
        }
        else
            battertyChargeText.text = ((int)batteryCharge).ToString();

        if (timeOnGrill >= grillTime[getIndex(placedFoodItemCode)])
        {
            Destroy(foodPrefabPlace.transform.GetChild(0).gameObject);
            GameObject spawnedModel = Instantiate(cookedFoodsPrefabs[getIndex(placedFoodItemCode)], foodPrefabPlace.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(foodPrefabPlace.transform);
            cooked = true;
            timeLeftText.text = "DONE";
            replaceCoals();
            handleFireSound();
        }
        else
        {
            int secondsLeft = (int) (grillTime[getIndex(placedFoodItemCode)] - timeOnGrill);
            timeLeftText.text = (secondsLeft / 60) + ":" + (secondsLeft % 60);
        }
    }

    int getIndex(int itemCode)
    {
        for (int i = 0; i < placeableFood.Length; i++)
            if (placeableFood[i] == itemCode)
                return i;

        return -1;
    }

    void replaceCoals()
    {
        GameObject spawnedModel;
        if (foodPlaced == true && cooked == false && batteryPlaced == true && batteryCharge > 0)
            spawnedModel = Instantiate(fireCoals, coalsPlace.transform.position, Quaternion.Euler(-90, 0, 0));
        else
            spawnedModel = Instantiate(coals, coalsPlace.transform.position, Quaternion.Euler(-90, 0, 0));

        Destroy(coalsPlace.transform.GetChild(0).gameObject);
        spawnedModel.transform.SetParent(coalsPlace.transform);
    }

    void handleFireSound()        //enables or disables the sound; DOES NOT CHANGE THE VOLUME
    {
        if (foodPlaced == true && cooked == false && batteryPlaced == true && batteryCharge > 0)
            fireSound.Play();
        else
            fireSound.Pause();
    }

    void buttonHintHandle()
    {
        if (selectedItemCode == 17)
        {
            if(batteryPlaced == false)
                Button_Hint.setBuildingInteractionHint("Place\n'Battery'");
            else
                Button_Hint.setBuildingInteractionHint("Collect\n'Battery'");
        }
        else
        {
            if (getIndex(selectedItemCode) != -1 && foodPlaced == false)
            {
                string text = "Place '" + ItemsList.getName(selectedItemCode) + "'";
                Button_Hint.setBuildingInteractionHint(text);
            }
            else if (foodPlaced == true && cooked == true)
            {
                string text = "Collect '" + ItemsList.getName(cookedFoodItemCodes[getIndex(placedFoodItemCode)]) + "'";
                Button_Hint.setBuildingInteractionHint(text);
            }
            else if (batteryPlaced == true)
                Button_Hint.setBuildingInteractionHint("Collect 'Battery'");
            else
                Button_Hint.clearBuildingInteractionHint();
        }
    }

    void changeFireSoundVolume(float volume)
    {
        fireSound.volume = maxFireSoundVolume * volume;
    }

    void OnDestroy()
    {
        SoundsManager.onSFxVolumeChange -= changeFireSoundVolume;
    }
}
