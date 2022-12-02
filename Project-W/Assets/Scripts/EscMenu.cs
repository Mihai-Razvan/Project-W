using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour       //this script is placed on canvas, not on esc menu
{
    [SerializeField]
    GameObject escMenuTab;
    static string status;

    void Start()
    {
        status = "CLOSED";
        escMenuTab.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (status.Equals("CLOSED"))
            {
                if (ActionLock.getActionLock().Equals("UNLOCKED"))
                {
                    escMenuTab.SetActive(true);
                    status = "OPENED";
                    ActionLock.setActionLock("UI_OPENED");
                    UnityEngine.Cursor.visible = true;
                }
            }
            else
            {
                FindObjectOfType<EscMenuButtons>().closeTabs();          //if you press esc while having a warning tab or settings tab opened it closes them so the aren't opened when you open esc menu next time
                closeMenu();
            }
        }
    }

    public void closeMenu()            //used for the back button is escMenuButtons script; also used above
    {
        escMenuTab.SetActive(false);
        status = "CLOSED";
        ActionLock.setActionLock("UNLOCKED");
        UnityEngine.Cursor.visible = false;
    }  
}
