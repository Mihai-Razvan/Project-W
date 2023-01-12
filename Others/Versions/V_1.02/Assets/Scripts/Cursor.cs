using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    bool visible;        //false for game scene and true for main menu scene

    void Start()
    {
        UnityEngine.Cursor.visible = visible;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;      //so the cursor don't go on secondary monitor
    }
}
