using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EscMenuButtons : MonoBehaviour      
{
    [SerializeField]
    GameObject menuWarningTab;
    [SerializeField]
    GameObject exitWarningTab;
    [SerializeField]
    GameObject settingsTab;            

    [SerializeField]
    GameObject settingsControlsTab;    
    [SerializeField]
    GameObject settingsAudioTab;     

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

    public void backButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        FindObjectOfType<EscMenu>().closeMenu();
    }

    public void menuButtonAction()
    {
        menuWarningTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsButtonAction()
    {
        settingsTab.SetActive(true);
        settingsControlsButtonAction();
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void exitButtonAction()
    {
        exitWarningTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    /// //////////////////////////////////////////////////////////

    public void menuWarningYesButtonAction()
    {
        SceneManager.LoadScene("MainMenuScene");
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void menuWarningNoButtonAction()
    {
        menuWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void exitWarningYesButtonAction()
    {
        Application.Quit();
    }

    public void exitWarningNoButtonAction()
    {
        exitWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    /////////////////////////////////////////////////// the ones under are fore settings which is also shared by the main menu scene ///////////////////////////////////////////////////////////
    ///
    public void settingsControlsButtonAction()
    {
        settingsAudioTab.SetActive(false);
        settingsControlsTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsAudioButtonAction()
    {
        settingsControlsTab.SetActive(false);
        settingsAudioTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void settingsBackButtonAction()
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
