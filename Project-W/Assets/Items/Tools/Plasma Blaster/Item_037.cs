using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_037 : Item       //asteroid destroyer
{
    [SerializeField]
    float maxChargeTime;
    float chargeTime;
    string laserState;            
    Transform projectileStartPosition;
    GameObject beamStartEffect;
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    AudioSource shootSound;
    [SerializeField]
    float maxShootSoundVolume;


    void Start()
    {
        laserState = "UNUSED";
        chargeTime = 0;

        changeShootSoundVolume(FindObjectOfType<SoundsManager>().getSFxVolume());
        SoundsManager.onSFxVolumeChange += changeShootSoundVolume;
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
                        shootSound.Play();
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

        ChargeRadial.setCharge(chargeTime, maxChargeTime, true);
    }

    void shoot()
    {
        GameObject spawnedObject = Instantiate(projectilePrefab, projectileStartPosition.position, projectileStartPosition.rotation);
    }

    void changeShootSoundVolume(float volume)
    {
        shootSound.volume = maxShootSoundVolume * volume;
    }

    void OnDestroy()
    {
        SoundsManager.onSFxVolumeChange -= changeShootSoundVolume;
    }
}
