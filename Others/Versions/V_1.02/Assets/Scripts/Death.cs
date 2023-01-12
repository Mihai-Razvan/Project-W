using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour      //it handles death conditions like health decrease, falling under skies; the script is placed on scripts manager
{
    [SerializeField]
    GameObject deathTab;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform cloudsLayer;
    static string status;

    void Start()
    {
        deathTab.SetActive(false);
        status = "ALIVE";
    }

    void Update()
    {
        fallDeath();
    }

    void fallDeath()
    {
        if (player.position.y < cloudsLayer.position.y)
            die();
    }

    public void die()
    {
        if(status.Equals("ALIVE"))
        {
            ActionLock.setActionLock("UI_OPENED");
            UnityEngine.Cursor.visible = true;
            deathTab.SetActive(true);
            status = "DEAD";
            AudioListener.volume = 0;
        }
    }

    public static void setDeathStatus(string newStatus)
    {
        status = newStatus;
    }
}
