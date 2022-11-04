using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_016 : MonoBehaviour
{
    [SerializeField]
    Animator bigCircleAnimator;
    [SerializeField]
    Animator smallCircleAnimator;
    [SerializeField]
    GameObject batteryHole;
    [SerializeField]
    GameObject batteryPrefab;
    [SerializeField]
    TextMeshProUGUI chargeText;
    [SerializeField]
    TextMeshProUGUI windText;

    bool batteryPlaced;
    [Range(0, 100)]
    float batteryCharge;
    float timeSinceCharged;

    void Start()
    {
        chargeText.text = "-";
        windText.text = "10";
        FindObjectOfType<Wind>().onWindSpeedChange += rotateBlades;
    }

    void Update()
    {
        if (FindObjectOfType<Interactions>().getInRangeBuilding() == this.gameObject && !FindObjectOfType<Player>().getActionLock().Equals("INVENTORY_OPENED"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interaction();
            }
        }

        charge();
    }

    void interaction()
    {
        if(batteryPlaced == true)    
        {
            int addedSlot = FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().addItem(17, 1);
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().setCharge(addedSlot, batteryCharge);
            Destroy(batteryHole.transform.GetChild(0).gameObject);
            batteryPlaced = false;
            chargeText.text = "-";
        }
        else if(FindObjectOfType<Player_Inventory>().getSelectedItemCode() == 17)   //battery
        {
            int playerInventorySlot = FindObjectOfType<Player_Inventory>().getSelectedSlot();
            batteryCharge = FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().getCharge(playerInventorySlot);
            FindObjectOfType<Player_Inventory>().getPlayerInventoryHolder().GetComponent<Inventory>().decreaseQuantity(1, playerInventorySlot);
            GameObject spawnedModel = Instantiate(batteryPrefab, batteryHole.transform.position, Quaternion.identity);
            spawnedModel.transform.SetParent(batteryHole.transform);
            batteryPlaced = true;
            timeSinceCharged = 0;
            chargeText.text = ((int)batteryCharge).ToString();
        }
    }

    void charge()
    {
        if(batteryPlaced == true && batteryCharge < 100)
        {
            timeSinceCharged += Time.deltaTime;
            if(timeSinceCharged >= 0.2f)
            {
                batteryCharge += 1;
                timeSinceCharged = 0;
                chargeText.text = ((int) batteryCharge).ToString();
            }
        }
    }

    void rotateBlades()
    {
        float animationSpeed = FindObjectOfType<Wind>().getSpeed() / 10;
        bigCircleAnimator.speed = animationSpeed;
        smallCircleAnimator.speed = animationSpeed * 3;
        windText.text = FindObjectOfType<Wind>().getSpeed().ToString();
    }
}
