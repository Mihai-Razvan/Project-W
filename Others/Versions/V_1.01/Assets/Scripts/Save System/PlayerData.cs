using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float saturation;
    public float thirst;
    public float health;
    public float xPos;
    public float yPos;
    public float zPos;

    public PlayerData(Player_Stats player_Stats, Player player)
    {
         saturation = player_Stats.getSaturation();
         thirst = player_Stats.getThirst();
         health = player_Stats.getHealth();
         xPos = player.getPlayerPosition().x;
         yPos = player.getPlayerPosition().y;
         zPos = player.getPlayerPosition().z;
    }
}
