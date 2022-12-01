using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EscMenuButtons : MonoBehaviour       //it also have the methods for the main menu scene settings panel
{
    [SerializeField]
    GameObject menuWarningTab;
    [SerializeField]
    GameObject exitWarningTab;
    [SerializeField]
    GameObject settingsTab;              //also for the main menu scene

    [SerializeField]
    GameObject settingsControlsTab;      //also for the main menu scene
    [SerializeField]
    GameObject settingsAudioTab;      //also for the main menu scene

    void Start()
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
        settingsTab.SetActive(false);
    }

    public void backButton()
    {
        FindObjectOfType<EscMenu>().closeMenu();
    }

    public void menuButton()
    {
        menuWarningTab.SetActive(true);
    }

    public void settingsButton()
    {
        settingsTab.SetActive(true);
        settingsControlsButton();
    }

    public void exitButton()
    {
        exitWarningTab.SetActive(true);
    }

    /// //////////////////////////////////////////////////////////

    public void menuWarningYesButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningNoButton()
    {
        menuWarningTab.SetActive(false);
    }

    public void exitWarningYesButton()
    {
        Application.Quit();
    }

    public void exitWarningNoButton()
    {
        exitWarningTab.SetActive(false);
    }

    /////////////////////////////////////////////////// the ones under are fore settings which is also shared by the main menu scene ///////////////////////////////////////////////////////////
    ///
    public void settingsControlsButton()
    {
        settingsAudioTab.SetActive(false);
        settingsControlsTab.SetActive(true);
    }

    public void settingsAudioButton()
    {
        settingsControlsTab.SetActive(false);
        settingsAudioTab.SetActive(true);
    }

    public void settingsBackButton()
    {
        settingsTab.SetActive(false);
    }
}
