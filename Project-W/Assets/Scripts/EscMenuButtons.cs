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

    [SerializeField]
    Slider uiVolumeSlider;
    [SerializeField]
    Slider ambienceVolumeSlider;
    [SerializeField]
    Slider sfxVolumeSlider;

    void Start()
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
        settingsTab.SetActive(false);
    }

    public void backButton()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        FindObjectOfType<EscMenu>().closeMenu();
    }

    public void menuButton()
    {
        menuWarningTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsButton()
    {
        settingsTab.SetActive(true);
        settingsControlsButton();
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void exitButton()
    {
        exitWarningTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    /// //////////////////////////////////////////////////////////

    public void menuWarningYesButton()
    {
        SceneManager.LoadScene("MainMenuScene");
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void menuWarningNoButton()
    {
        menuWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void exitWarningYesButton()
    {
        Application.Quit();
    }

    public void exitWarningNoButton()
    {
        exitWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    /////////////////////////////////////////////////// the ones under are fore settings which is also shared by the main menu scene ///////////////////////////////////////////////////////////
    ///
    public void settingsControlsButton()
    {
        settingsAudioTab.SetActive(false);
        settingsControlsTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsAudioButton()
    {
        settingsControlsTab.SetActive(false);
        settingsAudioTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsBackButton()
    {
        settingsTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void closeTabs()         //if you press esc while having a warning tab or settings tab opened it closes them so the aren't opened when you open esc menu next time
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
        settingsTab.SetActive(false);
    }

    public void onUiVolumeChange()
    {
        SoundsManager.onUiVolumeChange(uiVolumeSlider.value);
    }

    public void onAmbienceVolumeChange()
    {
        SoundsManager.onAmbienceVolumeChange(ambienceVolumeSlider.value); 
    }

    public void onSFxeVolumeChange()
    {
        SoundsManager.onSFxVolumeChange(sfxVolumeSlider.value);
    }
}
