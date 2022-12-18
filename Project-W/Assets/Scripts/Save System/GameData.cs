using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public PlayerData playerData;
    public PlayerInventoryData playerInventoryData;
    public List<PlaceableData> structuresData;
    public List<Item_016_SaveData> item_016_Data;       //wind turbine
    public List<Item_014_SaveData> item_014_Data;       //crop plot
    public List<Item_019_SaveData> item_019_Data;       //grill
    public List<Item_020_SaveData> item_020_Data;       //collection vacuum
    public List<Item_021_SaveData> item_021_Data;       //atmosphere condensator
    public List<Item_024_SaveData> item_024_Data;       //tree crop plot
    public List<Item_028_SaveData> item_028_Data;       //chest

    public GameData()
    {
        playerData = new PlayerData(GameDataHelper.getPlayerStats(), GameDataHelper.getPlayer());
        playerInventoryData = new PlayerInventoryData(Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().getSaveData());
        structuresData = PlaceableData.getPlaceables(GameDataHelper.structuresItemCodes);
        item_016_Data = Item_016_SaveData.getItemsData();
        item_014_Data = Item_014_SaveData.getItemsData();
        item_019_Data = Item_019_SaveData.getItemsData();
        item_020_Data = Item_020_SaveData.getItemsData();
        item_021_Data = Item_021_SaveData.getItemsData();
        item_024_Data = Item_024_SaveData.getItemsData();
        item_028_Data = Item_028_SaveData.getItemsData();
    }
}
