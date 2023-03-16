using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Player controller script that helps the user control the player over the networking and relay the same.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed;
    private float resetSpeed;
    public float dashSpeed;
    public float dashTime; 

    PhotonView view;

    public Animator playerAnim;
    public Transform player;

    Health health;

    LineRenderer rend;

    public float minX, maxX, minY, maxY;

    public TMP_Text nameDisplay;

    /// <summary>
    /// Called at start of frame
    /// </summary>
    private void Start()
    {
        resetSpeed = speed;

        view = GetComponent<PhotonView>();
        player = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
        health = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();

        if (view.IsMine) // checking if the current player is local player.
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = view.Owner.NickName;
        }
    }

    /// <summary>
    /// Called at every frame
    /// </summary>
    private void Update()
    {
        if (view.IsMine == true)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            Wrap(); // helps in screenwrap

            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine(Dash()); // dashing
            }

            if (moveInput == Vector2.zero)
            {
                playerAnim.SetBool("isMoving", false); // animation
            }
            else
            {
                playerAnim.SetBool("isMoving", true); // animation

                // animation scaling
                if (moveInput.x == -1)
                {
                    player.localScale = new Vector3(-1, player.localScale.y, player.localScale.z);
                    nameDisplay.gameObject.transform.localScale = new Vector3(-1, nameDisplay.gameObject.transform.localScale.y, nameDisplay.gameObject.transform.localScale.z);
                }
                else if (moveInput.x == 1)
                {
                    player.localScale = new Vector3(1, player.localScale.y, player.localScale.z);
                    nameDisplay.gameObject.transform.localScale = new Vector3(1, nameDisplay.gameObject.transform.localScale.y, nameDisplay.gameObject.transform.localScale.z);
                }
            }

            rend.SetPosition(0, transform.position);
        }
        else
        {
            rend.SetPosition(1, transform.position);
        }
    }

    /// <summary>
    /// Dash functionality
    /// </summary>
    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    /// <summary>
    /// Screen wrap functionality
    /// </summary>
    void Wrap()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    /// <summary>
    /// Check if player is colliding with enemy and then reduce the health
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        { 
            if (collision.tag == "Enemy")
            {
                health.TakeDamage();
            }
        }
    }
}
