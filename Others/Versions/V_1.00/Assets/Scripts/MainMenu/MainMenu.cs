using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject settingsTab;

    void Start()
    {
        settingsTab.SetActive(false);
    }

    public void playButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        SceneManager.LoadScene("GameScene");
    }

    public void exitButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        Application.Quit();
    }

    public void settingsButtonAction()
    {
        settingsTab.SetActive(true);
        FindObjectOfType<SettingsTab>().controlsButton();
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public GameObject getSettingsTab()
    {
        return settingsTab;
    }
}
