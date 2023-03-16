using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Helps in spawning the enemies
/// </summary>
public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float startTimeBtwSpawns;
    float timeBtwSpawns;

    /// <summary>
    /// Called at start of game
    /// </summary>
    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    /// <summary>
    /// Called at every frame of game
    /// </summary>
    private void Update()
    {
        // check if there is no master client or if players are less that room max player
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return; // do nothing
        }

        // if timer between spawns reaches 0
        if (timeBtwSpawns <= 0)
        {
            // take a random position and spawn the enemy
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            // else reduce the time to time between spawns
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
