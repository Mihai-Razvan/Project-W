using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_037 : Item
{
    [SerializeField]
    float maxChargeTime;
    float chargeTime;
    [SerializeField]
    float raySpeed;    //the speed at which the laser is expansing or retrating
    [SerializeField]
    float rayRadius;               //the radius for the capsule collider that si cheking for the ray collision
    string laserState;            
    Transform projectileStartPosition;
    GameObject beamStartEffect;
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    AudioSource raySound;
    [SerializeField]
    float maxRaySoundVolume;


    void Start()
    {
        laserState = "UNUSED";
        chargeTime = 0;

        changeRaySoundVolume(FindObjectOfType<SoundsManager>().getSFxVolume());
        SoundsManager.onSFxVolumeChange += changeRaySoundVolume;
    }

    void Update()
    {
        if (getUsedObject() != null && ActionLock.getActionLock().Equals("UI_OPENED") == false)
        {
            beamStartEffect = getUsedObject().transform.GetChild(0).transform.GetChild(1).gameObject;
            projectileStartPosition = getUsedObject().transform.GetChild(0).transform.GetChild(0);

            if (laserState.Equals("UNUSED"))
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    laserState = "CHARGING";
                    beamStartEffect.SetActive(true);
                    ActionLock.setActionLock("ACTION_LOCKED");
                }
                else
                    beamStartEffect.SetActive(false);

            }
            else        //else is CHARGING
            {
                chargeLaser();

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if(chargeTime >= maxChargeTime)
                    {
                        shoot();               
                        raySound.Play();
                    }

                    chargeTime = 0;
                    laserState = "UNUSED";
                    ChargeRadial.resetCharge();
                    ActionLock.setActionLock("UNLOCKED");
                }
            }
        }
    }

    void chargeLaser()
    {
        chargeTime += Time.deltaTime;

        if (chargeTime >= maxChargeTime)
            chargeTime = maxChargeTime;    //in case charge time is greater

        FindObjectOfType<ChargeRadial>().setCharge(chargeTime, maxChargeTime);
    }

    void shoot()
    {
        GameObject spawnedObject = Instantiate(projectilePrefab, projectileStartPosition.position, projectileStartPosition.rotation);
    }

    void changeRaySoundVolume(float volume)
    {
        raySound.volume = maxRaySoundVolume * volume;
    }

    void OnDestroy()
    {
        SoundsManager.onSFxVolumeChange -= changeRaySoundVolume;
    }
}
