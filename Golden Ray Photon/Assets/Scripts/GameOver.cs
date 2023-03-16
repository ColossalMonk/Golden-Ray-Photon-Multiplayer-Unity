using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

/// <summary>
/// Allows the master client to restart the game if needed otherwise quit the game
/// </summary>
public class GameOver : MonoBehaviour
{
    public Score scoreScript;
    public TMP_Text scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;

    PhotonView view;

    /// <summary>
    /// Called at the start of game
    /// </summary>
    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient == false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
        else
        {
            restartButton.SetActive(true);
            waitingText.SetActive(false);
        }
    }

    /// <summary>
    /// Called at every frame
    /// </summary>
    private void Update()
    {
        scoreDisplay.text = scoreScript.score.ToString();
    }

    /// <summary>
    /// Targerting the restart button for all the members in the room
    /// </summary>
    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    /// <summary>
    /// Helps sync and reload the scene for all the participants in the room
    /// </summary>
    [PunRPC]
    void Restart()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
}
