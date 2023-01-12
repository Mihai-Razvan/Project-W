using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuButtons : MonoBehaviour          //this script is attached to canvas->death tab
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

    [SerializeField]
    LayerMask placeableMask;
    [SerializeField]
    GameObject foundationPrefab;

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
        SaveSystem.Save();
        AudioListener.volume = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningNoButtonAction()
    {
        AudioListener.volume = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void menuWarningCancelButtonAction()
    {
        menuWarningTab.SetActive(false);
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
    }

    //////////////////////////////////////


    void respawn()
    {
        FindObjectOfType<Player_Stats>().setSaturation(50);
        FindObjectOfType<Player_Stats>().setThirst(50);
        FindObjectOfType<Player_Stats>().setHealth(100);

        for (int i = 0; i < 23; i++)
            Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().setSlot(i, 0, 0, 0);
         
        Player_Inventory.getPlayerInventoryHolder().GetComponent<Inventory>().setSlot(0, 2, 1, 0);       //gives you the grappler

        FindObjectOfType<PlayerMovement>().setPlayerPosition(chooseRespawnPoint());

        UnityEngine.Cursor.visible = false;
        ActionLock.setActionLock("UNLOCKED");
        Death.setDeathStatus("ALIVE");
    }

    Vector3 chooseRespawnPoint()
    {
        bool foundRespawnPoint = false;
        bool foundationsExist = false;

        Collider[] colliders = Physics.OverlapSphere(new Vector3(0, 75, 0), 380, placeableMask);
        for(int i = 0; i < colliders.Length; i++)
            if(colliders[i].gameObject.tag.Equals("Item_004"))
            {
                foundationsExist = true;
                break;
            }

        if(foundationsExist == false)          //in case all foundations get destroyed you get back the 4 default ones
        {
            Instantiate(foundationPrefab, new Vector3(1.9f, 75, -1.9f), Quaternion.identity);
            Instantiate(foundationPrefab, new Vector3(-1.9f, 75, -1.9f), Quaternion.identity);
            Instantiate(foundationPrefab, new Vector3(-1.9f, 75, 1.9f), Quaternion.identity);
            Instantiate(foundationPrefab, new Vector3(1.9f, 75, 1.9f), Quaternion.identity);
        }

        Vector3 respawnPoint = respawnPosition.position;
        RaycastHit[] hits = Physics.RaycastAll(respawnPosition.position, -transform.up, 1000, placeableMask);
        if (hits.Length != 0)
            foundRespawnPoint = true;

        while (foundRespawnPoint == false)
        {
            respawnPoint = Random.insideUnitSphere * 380 + new Vector3(0, 480, 0);
            hits = Physics.RaycastAll(respawnPoint, -transform.up, 1000, placeableMask);

            if (hits.Length != 0)
                foundRespawnPoint = true;
        }

        return new Vector3(respawnPoint.x, respawnPosition.position.y, respawnPoint.z);
    }
}
