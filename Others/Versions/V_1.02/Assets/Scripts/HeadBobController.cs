using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField]
    float bobFrequency;
    [SerializeField] 
    float maxWalkingBobbingAmount;
    [SerializeField]
    float maxRunnincBobbingAmount;
    float bobbingValue;      //the value set by player in settings
    float bobbingAmount;     // the product between the max values and the value set by player in settings    

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        bobbingValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Player is moving
            if (PlayerMovement.getMovementState().Equals("WALKING"))
                bobbingAmount = maxWalkingBobbingAmount * bobbingValue;
            else
                bobbingAmount = maxRunnincBobbingAmount * bobbingValue;

            timer += Time.deltaTime * bobFrequency;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * bobFrequency), transform.localPosition.z);
        }
    }

    public void changeBobAmount(float value)      //used when you change the bobAmount in settings
    {
        bobbingValue = value;
    }
}