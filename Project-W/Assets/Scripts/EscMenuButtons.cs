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

    public void saveButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        SaveSystem.Save();
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
        SaveSystem.Save();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningNoButtonAction()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningCancelButtonAction()
    {
        menuWarningTab.SetActive(false);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void exitWarningYesButtonAction()
    {
        SaveSystem.Save();
        Application.Quit();
    }

    public void exitWarningNoButtonAction()
    {
        Application.Quit();
    }

    public void exitWarningCancelButtonAction()
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
     //   SaveSystem.SaveSettings();
    }

    public void onAmbienceVolumeChange()
    {
        SoundsManager.onAmbienceVolumeChange(ambienceVolumeSlider.value);
      //  SaveSystem.SaveSettings();
    }

    public void onSFxeVolumeChange()
    {
        SoundsManager.onSFxVolumeChange(sfxVolumeSlider.value);
     //   SaveSystem.SaveSettings();
    }



  /*  public List<float> saveData()
    {
        return new List<float>() { ambienceVolumeSlider.value, sfxVolumeSlider.value, uiVolumeSlider.value };
    }

    public void loadData()         //this is done using PlayerPrefs, so it isn't linked to the file save system, excepting SaveSettings() ehich is in SaveSysyem class
    {
        if (PlayerPrefs.HasKey("Settings_Sound_Ambience_Volume"))
        {
            ambienceVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_Ambience_Volume");
            SoundsManager.onAmbienceVolumeChange(PlayerPrefs.GetFloat("Settings_Sound_Ambience_Volume"));
        }

        if (PlayerPrefs.HasKey("Settings_Sound_SFX_Volume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_SFX_Volume");
            SoundsManager.onSFxVolumeChange(PlayerPrefs.GetFloat("Settings_Sound_SFX_Volume"));
        }

        if (PlayerPrefs.HasKey("Settings_Sound_UI_Volume"))
        {
            uiVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_UI_Volume");
            SoundsManager.onUiVolumeChange(PlayerPrefs.GetFloat("Settings_Sound_UI_Volume"));
        }
    }*/
}
