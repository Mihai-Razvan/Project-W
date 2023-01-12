using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour           //this script is placed on scripts manager -> saveSystem
{
    public static GameData loadedData;

    [SerializeField]
    GameObject foundationPrefab;          //used to instantiate the initial foundations when you create a new world

    void Start()
    {
        if (PlayerPrefs.HasKey("Save_Exist") && PlayerPrefs.GetString("Save_Exist").Equals("True"))  //there is a save; otherwise it means it's a new world
        {
            loadedData = SaveSystem.Load();

            if (loadedData != null)
            {
                load_Player_Data();
                load_Player_Inventory();
                load_Structures();
                load_item_014();
                load_item_016();
                load_item_019();
                load_item_020();
                load_item_021();
                load_item_024();
                load_item_028();
                load_item_029();
                load_item_030();
                load_item_031();
                load_item_032();
            }
        }
        else
            loadNewWorld();                  
    }

    void loadNewWorld()
    {
        Instantiate(foundationPrefab, new Vector3(1.9f, 75, -1.9f), Quaternion.identity);
        Instantiate(foundationPrefab, new Vector3(-1.9f, 75, -1.9f), Quaternion.identity);
        Instantiate(foundationPrefab, new Vector3(-1.9f, 75, 1.9f), Quaternion.identity);
        Instantiate(foundationPrefab, new Vector3(1.9f, 75, 1.9f), Quaternion.identity);

        SaveSystem.Save();
    }

    void load_Player_Data()
    {
        PlayerData data = loadedData.playerData;

        if (data == null)
            return;

        FindObjectOfType<Player>().loadData(new Vector3(data.xPos, data.yPos, data.zPos));     //loads player position
        FindObjectOfType<Player_Stats>().loadData(data.saturation, data.thirst, data.health);     //loads player stats
    }

    void load_Player_Inventory()
    {
        PlayerInventoryData data = loadedData.playerInventoryData;

        if (data == null)
            return;

        FindObjectOfType<Player_Inventory>().loadData();
    }

    void load_Structures()
    {
        List<PlaceableData> data = loadedData.structuresData;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            Instantiate(GameDataHelper.getStructures(data[i].itemCode), pos, Quaternion.Euler(rotation));
        }
    }

    void load_item_014()
    {
        List<Item_014_SaveData> data = loadedData.item_014_Data;

        if (data == null)  //this isn't necessary now but it will be required in the next versions so you can load a save from an old version on a new version 
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(14), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_014>().loadData(data[i].cropItemCodeArray, data[i].slotStatusArray, data[i].timeArray);
        }
    }

    void load_item_016()
    {
        List<Item_016_SaveData> data = loadedData.item_016_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(16), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_016>().loadData(data[i].batteryPlaced, data[i].batteryCharge, data[i].timeSinceCharged);
        }
    }

    void load_item_019()
    {
        List<Item_019_SaveData> data = loadedData.item_019_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(19), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_019>().loadData(data[i].placedFoodItemCode, data[i].batteryPlaced, data[i].foodPlaced, data[i].batteryCharge, data[i].timeOnGrill, data[i].cooked);
        }
    }

    void load_item_020()
    {
        List<Item_020_SaveData> data = loadedData.item_020_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(20), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_020>().loadData(data[i].collectedResourceCodes, data[i].resourceItemCodeArray, data[i].resourceQuantityArray, data[i].resourceChargeArray);
        }
    }

    void load_item_021()
    {
        List<Item_021_SaveData> data = loadedData.item_021_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(21), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_021>().loadData(data[i].batteryPlaced, data[i].batteryCharge, data[i].filled, data[i].timeSinceStarted);
        }
    }

    void load_item_024()
    {
        List<Item_024_SaveData> data = loadedData.item_024_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(24), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_024>().loadData(data[i].status, data[i].index, data[i].fruitItemCode, data[i].timeSincePlanted);
        }
    }

    void load_item_028()
    {
        List<Item_028_SaveData> data = loadedData.item_028_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            GameObject spawnedObject = Instantiate(GameDataHelper.getStructures(28), pos, Quaternion.Euler(rotation));
            spawnedObject.GetComponent<Item_028>().loadData(data[i].itemCodeArray, data[i].quantityArray, data[i].chargeArray);
        }
    }

    void load_item_029()
    {
        List<PlaceableData> data = loadedData.item_029_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            Instantiate(GameDataHelper.getStructures(29), pos, Quaternion.Euler(rotation));
        }
    }

    void load_item_030()
    {
        List<PlaceableData> data = loadedData.item_030_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            Instantiate(GameDataHelper.getStructures(30), pos, Quaternion.Euler(rotation));
        }
    }

    void load_item_031()
    {
        List<PlaceableData> data = loadedData.item_031_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            Instantiate(GameDataHelper.getStructures(31), pos, Quaternion.Euler(rotation));
        }
    }

    void load_item_032()
    {
        List<PlaceableData> data = loadedData.item_032_Data;

        if (data == null)
            return;

        for (int i = 0; i < data.Count; i++)
        {
            Vector3 pos = new Vector3(data[i].position[0], data[i].position[1], data[i].position[2]);
            Vector3 rotation = new Vector3(data[i].rotation[0], data[i].rotation[1], data[i].rotation[2]);
            Instantiate(GameDataHelper.getStructures(32), pos, Quaternion.Euler(rotation));
        }
    }
}
