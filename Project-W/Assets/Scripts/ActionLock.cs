using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLock : MonoBehaviour
{
    static string actionLock = "UNLOCKED";   //what actions are locked: ACTION_LOCK (can't open invenotry, settings etc but can move), "MOVE_LOCK"(or smth) can't moce but can open invenotr

    public static void setActionLock(string actionLock)
    {
        ActionLock.actionLock = actionLock;
    }

    public static string getActionLock()
    {
        return actionLock;
    }
}
