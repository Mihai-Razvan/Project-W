using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuButtons : MonoBehaviour
{
    [SerializeField]
    GameObject deathMenuTab;
    [SerializeField]
    Transform respawnPosition;

    [SerializeField]
    GameObject menuWarningTab;
    [SerializeField]
    GameObject menuWarningYesButton;
    [SerializeField]
    GameObject menuWarningNoButton;

    [SerializeField]
    GameObject exitWarningTab;
    [SerializeField]
    GameObject exitWarningYesButton;
    [SerializeField]
    GameObject exitWarningNoButton;

    private void Start()
    {
        menuWarningTab.SetActive(false);
        exitWarningTab.SetActive(false);
    }

    public void respawnButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        respawn();
        deathMenuTab.SetActive(false);
        AudioListener.volume = 1;
    }

    public void menuButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        menuWarningTab.SetActive(true);
    }

    public void exitButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        exitWarningTab.SetActive(true);
    }

    //////////////////////////////////////

    public void menuWarningYesButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        AudioListener.volume = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningNoButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        menuWarningTab.SetActive(false);
    }

    public void exitWarningYesButtonAction()
    {
        Application.Quit();
    }

    public void exitWarningNoButtonAction()
    {
        FindObjectOfType<SoundsManager>().playClickButtonSound();
        exitWarningTab.SetActive(false);
    }


    //////////////////////////////////////


    void respawn()
    {
        FindObjectOfType<Player_Stats>().setHealth(100);

        FindObjectOfType<PlayerMovement>().setPlayerPosition(respawnPosition.position);
        UnityEngine.Cursor.visible = false;
        ActionLock.setActionLock("UNLOCKED");
        Death.setDeathStatus("ALIVE");
    }
}
