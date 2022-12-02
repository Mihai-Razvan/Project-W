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
    AudioSource clickSound;

    void Start()
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
        settingsTab.SetActive(false);
    }

    public void backButton()
    {
        clickSound.Play();
        FindObjectOfType<EscMenu>().closeMenu();
    }

    public void menuButton()
    {
        menuWarningTab.SetActive(true);
        clickSound.Play();
    }

    public void settingsButton()
    {
        settingsTab.SetActive(true);
        settingsControlsButton();
        clickSound.Play();
    }

    public void exitButton()
    {
        exitWarningTab.SetActive(true);
        clickSound.Play();
    }

    /// //////////////////////////////////////////////////////////

    public void menuWarningYesButton()
    {
        SceneManager.LoadScene("MainMenuScene");
        clickSound.Play();
    }

    public void menuWarningNoButton()
    {
        menuWarningTab.SetActive(false);
        clickSound.Play();
    }

    public void exitWarningYesButton()
    {
        Application.Quit();
    }

    public void exitWarningNoButton()
    {
        exitWarningTab.SetActive(false);
        clickSound.Play();
    }

    /////////////////////////////////////////////////// the ones under are fore settings which is also shared by the main menu scene ///////////////////////////////////////////////////////////
    ///
    public void settingsControlsButton()
    {
        settingsAudioTab.SetActive(false);
        settingsControlsTab.SetActive(true);
        clickSound.Play();
    }

    public void settingsAudioButton()
    {
        settingsControlsTab.SetActive(false);
        settingsAudioTab.SetActive(true);
        clickSound.Play();
    }

    public void settingsBackButton()
    {
        settingsTab.SetActive(false);
        clickSound.Play();
    }

    public void closeTabs()         //if you press esc while having a warning tab or settings tab opened it closes them so the aren't opened when you open esc menu next time
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
        settingsTab.SetActive(false);
    }
}
