using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour      //provides the method for writing and loading data from the save file; this script is placed on scripts manager -> saveSystem
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/SaveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData();

        formatter.Serialize(stream, gameData);
        stream.Close();

        PlayerPrefs.SetString("Save_Exist", "True");
    }

    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/SaveData";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return gameData;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }

  /*  public static void SaveSettings()          //this are separated because they use the PlayerPrefs instead of the file sava system
    {
        List<float> data;

        if(SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
            data = FindObjectOfType<SettingsTab>().saveData();
        else
            data = FindObjectOfType<EscMenuButtons>().saveData();
      
        PlayerPrefs.SetFloat("Settings_Sound_Ambience_Volume", data[0]);
        PlayerPrefs.SetFloat("Settings_Sound_SFX_Volume", data[1]);
        PlayerPrefs.SetFloat("Settings_Sound_UI_Volume", data[2]);
    }*/
}
