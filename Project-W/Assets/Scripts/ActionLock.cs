using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLock : MonoBehaviour     //this script is placed on manager->scripts manager so Start() can run
{
    static string actionLock;  //what actions are locked: ACTION_LOCK (can't open invenotry, settings etc but can move), "MOVE_LOCK"(or smth) can't moce but can open invenotr

    void Start()
    {
        actionLock = "UNLOCKED";   //we put this on start instead of declearing above because otherwise it won't change back to "UNLOCKED" when going to menu and back to game scene
    }

    public static void setActionLock(string actionLock)
    {
        ActionLock.actionLock = actionLock;
    }

    public static string getActionLock()
    {
        return actionLock;
    }
}
