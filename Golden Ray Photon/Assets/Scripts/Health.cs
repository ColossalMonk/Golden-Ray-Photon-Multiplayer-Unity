using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

/// <summary>
/// Health management over the network as both player share common health
/// </summary>
public class Health : MonoBehaviour
{
    public int health = 10;
    public TMP_Text healthDisplay;

    PhotonView view;

    public GameObject gameOver;

    /// <summary>
    /// Called at start of game
    /// </summary>
    private void Start()
    {
        view = GetComponent<PhotonView>();
        gameOver.SetActive(false);
    }

    /// <summary>
    /// A public function to be called from other places
    /// </summary>
    public void TakeDamage()
    {
        view.RPC("TakeDamageRPC", RpcTarget.All);
    }

    /// <summary>
    /// This method helps in creating a single instance of health so that both players share a common health system
    /// </summary>
    [PunRPC]
    private void TakeDamageRPC()
    {
        health--;

        if (health <= 0)
        {
            gameOver.SetActive(true);
        }

        healthDisplay.text = health.ToString();
    }
}
