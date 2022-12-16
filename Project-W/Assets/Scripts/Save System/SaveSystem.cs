using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour      //provides the method for writing and loading data from the save file
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData();

        formatter.Serialize(stream, gameData);
        stream.Close();

        SaveSettings();
    }

    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/save.test";

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

    public static void SaveSettings()          //this are separated because they use the PlayerPrefs instead of the file sava system
    {
        List<float> data;

        if(SceneManager.GetActiveScene().name.Equals("MainMenuScene"))
            data = FindObjectOfType<SettingsTab>().saveData();
        else
            data = FindObjectOfType<EscMenuButtons>().saveData();

        PlayerPrefs.SetFloat("Settings_Sound_Ambience_Volume", data[0]);
        PlayerPrefs.SetFloat("Settings_Sound_SFX_Volume", data[1]);
        PlayerPrefs.SetFloat("Settings_Sound_UI_Volume", data[2]);
    }
}
