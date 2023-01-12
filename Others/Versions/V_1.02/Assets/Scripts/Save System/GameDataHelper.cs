using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataHelper : MonoBehaviour         //used to help GameData with methods and stuff
{
    public static List<int> structuresItemCodes;
    [SerializeField]
    List<int> structuresItemCodesHelper;

    public static List<GameObject> structuresPrefabs;
    [SerializeField]
    List<GameObject> structuresPrefabsHelper;

    public static List<int> buildingsItemCodes;
    [SerializeField]
    List<int> buildingsItemCodesHelper;

    public static List<GameObject> buildingsPrefabs;
    [SerializeField]
    List<GameObject> buildingsPrefabsHelper;

    void Awake()
    {
        structuresItemCodes = structuresItemCodesHelper;
        structuresPrefabs = structuresPrefabsHelper;

        buildingsItemCodes = buildingsItemCodesHelper;
        buildingsPrefabs = buildingsPrefabsHelper;
    }

    public static Player_Stats getPlayerStats()
    {
        return FindObjectOfType<Player_Stats>();
    }

    public static Player getPlayer()
    {
        return FindObjectOfType<Player>();
    }

    public static GameObject getStructures(int itemCode)
    {
        for (int i = 0; i < structuresItemCodes.Count; i++)
            if (structuresItemCodes[i] == itemCode)
                return structuresPrefabs[i];

        for (int i = 0; i < buildingsItemCodes.Count; i++)
            if (buildingsItemCodes[i] == itemCode)
                return buildingsPrefabs[i];

        return null;
    }
}
