using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button exitButton;
    [SerializeField]
    Button settingsButton;

    public void playButtonAction()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitButtonAction()
    {
        Application.Quit();
    }
}
