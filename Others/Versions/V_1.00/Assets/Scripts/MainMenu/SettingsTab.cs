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

    void Start()
    {

    }

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
