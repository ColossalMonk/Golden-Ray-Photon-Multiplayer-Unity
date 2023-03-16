using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Enemy Controller script which makes the enemy follow the nearest player over the network.
/// </summary>
public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speed;

    private Score score;

    public GameObject deathFx;
    PhotonView view;

    /// <summary>
    /// Called at start of frame.
    /// </summary>
    private void Start()
    {
        view = GetComponent<PhotonView>();

        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        // finding the nearest player and then moving towards that player
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        if (distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Checking for collision with the 'Golden Ray' and then destroying the player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "GoldenRay")
            {
                score.AddScore();
                view.RPC("SpawnParticle", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Creating the Particles over the network so that we can see on both the player.
    /// </summary>
    [PunRPC]
    private void SpawnParticle()
    {
        Instantiate(deathFx, transform.position, Quaternion.identity);
    }
}
