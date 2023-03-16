using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Score manager to display score
/// </summary>
public class Score : MonoBehaviour
{
    public int score = 0;
    PhotonView view;
    public TMP_Text scoreDisplay;

    /// <summary>
    /// Called at start of game
    /// </summary>
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Public function to add score over the network for both the players
    /// </summary>
    public void AddScore()
    {
        view.RPC("AddScoreRPC", RpcTarget.All);
    }

    /// <summary>
    /// This is a network function and helps manage and sync score over the network for all the players in room
    /// </summary>
    [PunRPC]
    private void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
