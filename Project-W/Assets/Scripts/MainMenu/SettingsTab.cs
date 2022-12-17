using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTab : MonoBehaviour    //handles the settings tab for the MAIN MENU
{
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

    public void controlsButton()
    {
        settingsAudioTab.SetActive(false);
        settingsControlsTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void audioButton()
    {
        settingsControlsTab.SetActive(false);
        settingsAudioTab.SetActive(true);
        FindObjectOfType<SoundsManager>().playClickButtonSound();
    }

    public void backButton()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        FindObjectOfType<MainMenu>().getSettingsTab().SetActive(false);
    }

    public void onUiVolumeChange()
    {
        SoundsManager.onUiVolumeChange(uiVolumeSlider.value);
     //   SaveSystem.SaveSettings();
    }

    public void onAmbienceVolumeChange()
    {
        SoundsManager.onAmbienceVolumeChange(ambienceVolumeSlider.value);
    //    SaveSystem.SaveSettings();
    }

    public void onSFxeVolumeChange()
    {
        SoundsManager.onSFxVolumeChange(sfxVolumeSlider.value);
    //    SaveSystem.SaveSettings();
    }

  /*  public List<float> saveData()
    {
        return new List<float>() { ambienceVolumeSlider.value, sfxVolumeSlider.value, uiVolumeSlider.value };
    }

    public void loadData()         //this is done using PlayerPrefs, so it isn't linked to the file save system, excepting SaveSettings() ehich is in SaveSysyem class
    {
        if(PlayerPrefs.HasKey("Settings_Sound_Ambience_Volume"))
        {
            ambienceVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_Ambience_Volume");
            onAmbienceVolumeChange();
        }

        if (PlayerPrefs.HasKey("Settings_Sound_SFX_Volume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_SFX_Volume");
            onSFxeVolumeChange();
        }

        if (PlayerPrefs.HasKey("Settings_Sound_UI_Volume"))
        {
            uiVolumeSlider.value = PlayerPrefs.GetFloat("Settings_Sound_UI_Volume");
            onUiVolumeChange();
        }     
    }*/
}
