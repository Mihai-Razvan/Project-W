using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour         //used to handle the change of volume for UI volume, music volume, master volume etc; it also manages music and ambience volume; the script is placed on SOUNDS MANAGER
{
    public delegate void OnUiVolumeChange(float volume);
    public static OnUiVolumeChange onUiVolumeChange;

    public delegate void OnAmbienceVolumeChange(float volume);
    public static OnAmbienceVolumeChange onAmbienceVolumeChange;

    public delegate void OnSFxVolumeChange(float volume);
    public static OnSFxVolumeChange onSFxVolumeChange;

    [SerializeField]
    Slider uiVolumeSlider;
    [SerializeField]
    Slider ambienceVolumeSlider;
    [SerializeField]
    Slider sfxVolumeSlider;

    //sounds handled here; not all sound are hanled here, for ex grill fire sound

    [SerializeField]
    AudioSource windSound;
    [SerializeField]
    float windSoundMaxVolume;

    [SerializeField]
    AudioSource placeSound;
    [SerializeField]
    float placeSoundMaxVolume;

    [SerializeField]
    AudioSource clickUiButtonSound;
    [SerializeField]
    float clickUiButtonSoundMaxVolume;

    [SerializeField]
    AudioSource collectItemSound;
    [SerializeField]
    float collectItemSoundMaxVolume;

    [SerializeField]
    AudioSource heartBeatSound;
    [SerializeField]
    float heartBeatSoundMaxVolume;


    void Start()
    {
        onAmbienceVolumeChange += changeWindVolume;
        onSFxVolumeChange += changePlacePlaceableVolume;
        onSFxVolumeChange += collectItemVolume;
        onSFxVolumeChange += heartBeatVolume;
        onUiVolumeChange += changeClickUiButtonVolume;
    }

    //manages the volume changers for the sounds handled here

    void changeWindVolume(float volume)
    {
        windSound.volume = windSoundMaxVolume * volume;
    }

    void changePlacePlaceableVolume(float volume)
    {
        placeSound.volume = placeSoundMaxVolume * volume;
    }

    void changeClickUiButtonVolume(float volume)
    {
        clickUiButtonSound.volume = clickUiButtonSoundMaxVolume * volume;
    }

    void collectItemVolume(float volume)
    {
        collectItemSound.volume = collectItemSoundMaxVolume * volume;
    }

    void heartBeatVolume(float volume)
    {
        heartBeatSound.volume = heartBeatSoundMaxVolume * volume;
    }

    //play different sounds

    public void playPlacePlaceableSound()
    {
        placeSound.Play();
    }

    public void playClickButtonSound()
    {
        clickUiButtonSound.Play();
    }

    public void playCollectItemSound()
    {
        collectItemSound.Play();
    }

    public void playHeartBeatSound()
    {
        heartBeatSound.Play();
    }

    //pause different sounds

    public void pauseHeartBeatSound()
    {
        heartBeatSound.Pause();
    }

    //the getters are used when we create an object and we want to set its sound in Start(); otherwise the sound will be 100% in start no matter the slider value 

    public float getUiVolume()
    {
        return uiVolumeSlider.value;
    }

    public float getAmbienceVolume()
    {
        return ambienceVolumeSlider.value;
    }

    public float getSFxVolume()
    {
        return sfxVolumeSlider.value;
    }

    void OnDestroy()
    {
        onAmbienceVolumeChange -= changeWindVolume;
        onSFxVolumeChange -= changePlacePlaceableVolume;
        onSFxVolumeChange -= collectItemVolume;
        onSFxVolumeChange -= heartBeatVolume;
        onUiVolumeChange -= changeClickUiButtonVolume;
    }
}
