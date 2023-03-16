using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Helps in creating the players in a certain range within the screen.
/// </summary>
public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    public float minX, minY, maxX, maxY;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity); // helps placing the player anywhere in the desired position over the network.
    }
}
