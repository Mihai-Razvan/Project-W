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

    void Start()
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
    }

    public void backButton()
    {
        FindObjectOfType<EscMenu>().closeMenu();
    }

    public void menuButton()
    {
        menuWarningTab.SetActive(true);
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
}
