using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MultiplayersButtons : MonoBehaviour       //this script is placed on manager -> multiplayer manager
{
    public void hostButtonClick()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void clientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}
