using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject settingsTab;
    [SerializeField]
    GameObject loadWorldButton;          //we need this to disable it when we have no
    [SerializeField]
    GameObject newWorldWarningTab;

    void Start()
    {
        settingsTab.SetActive(false);
        newWorldWarningTab.SetActive(false);
        loadWorldButton.GetComponent<Button>().interactable = checkSaveExist();
    }

    public void newWorldButtonAction()
    {
        if(checkSaveExist() == true)             //if there is already a save it shows the warning menu
            newWorldWarningTab.SetActive(true);
        else           //otherwise it send you to the game scene as a new save
        {
            PlayerPrefs.SetString("Save_Exist", "False");
            SceneManager.LoadScene("GameScene");
        }
    }

    public void loadWorldButtonAction()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitButtonAction()
    {
        Application.Quit();
    }

    public void settingsButtonAction()
    {
        settingsTab.SetActive(true);
        FindObjectOfType<SettingsTab>().controlsButton();
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void newWorldWarningYesButtonAction()
    {
        PlayerPrefs.SetString("Save_Exist", "False");
        SceneManager.LoadScene("GameScene");
    }

    public void newWorldWarningNoButtonAction()
    {
        newWorldWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    ////////////////////////////////////////////////////////////////

    public GameObject getSettingsTab()
    {
        return settingsTab;
    }

    bool checkSaveExist()       //it isn't enought to have a variable in playerPrefas, we also have to do a check in case the file gets deleted, outside of the game case in which the playerPref won't be changed
    { 
        string path = Application.persistentDataPath + "/SaveData";

        if (File.Exists(path))
        {
            PlayerPrefs.SetString("Save_Exist", "True");
            return true;
        }
        else
        {
            PlayerPrefs.SetString("Save_Exist", "False");
            return false;
        }
    }
}
