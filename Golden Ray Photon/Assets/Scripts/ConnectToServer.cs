using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that helps us to connect the photon server
/// </summary>
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Start is called on start frame
    /// </summary>
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Called when the client connects to the Master Server and is ready for matchmaking
    /// </summary>
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
