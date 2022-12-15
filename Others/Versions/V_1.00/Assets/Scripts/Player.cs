using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if (ActionLock.getActionLock().Equals("UI_OPENED") == false)          //if we don't do this in this class and put this in CameraLook Update some strange things happens with grappler ray
            CameraLook.rotateCamera();
    }
}
