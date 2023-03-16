using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// This script is getting called when you have to enter room name to either create a room or to join a room
/// along with the functionality to give a nickname to the character
/// </summary>
public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public TMP_Text errorText;

    public TMP_InputField nameInput;

    /// <summary>
    /// Start is called on start frame
    /// </summary>
    private void Start()
    {
        errorText.text = "";
    }


    /// <summary>
    /// Helps user to input the nickname
    /// </summary>
    public void ChangeName()
    {
        if (nameInput.text != "")
        {
            // Sets the nickname of the player the for the display to other user in same room.
            PhotonNetwork.NickName = nameInput.text;
        }
    }

    /// <summary>
    /// Helps the master client creat a room
    /// </summary>
    public void CreateRoom()
    {
        if (createInput.text != "")
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2; // limits the max number of players to 2 in a certain room at any point

            PhotonNetwork.CreateRoom(createInput.text); // server command to create the room
        }
        else
        {
            StartCoroutine("ShowCreateRoomError"); // this is when room are left without name and then player tries to enter the room
            if (nameInput.text == "")
            {
                StartCoroutine("ShowNameInputError"); // this is when nickname area is empty
            }
        }
    }

    /// <summary>
    /// Helps the other player to join the master room
    /// </summary>
    public void JoinRoom()
    {
        if (joinInput.text != "")
        {
            PhotonNetwork.JoinRoom(joinInput.text); // server command to join the room
        }
        else
        {
            StartCoroutine("ShowJoinRoomError"); // this is when room are left without name and then player tries to enter the room
            if (nameInput.text == "")
            {
                StartCoroutine("ShowNameInputError"); // this is when nickname area is empty
            }
        }
    }

    /// <summary>
    /// Switching the room once the values are filled in
    /// </summary>
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    /// <summary>
    /// Shows the error text when the Create room field is empty and player tries to move forward
    /// </summary>
    IEnumerator ShowCreateRoomError()
    {
        errorText.text = "Create Room field is missing, please check values";
        yield return new WaitForSeconds(3f);
        errorText.text = "";
    }

    /// <summary>
    /// Shows the error text when the Join room field is empty and player tries to move forward
    /// </summary>
    IEnumerator ShowJoinRoomError()
    {
        errorText.text = "Join Room field is missing, please check values";
        yield return new WaitForSeconds(3f);
        errorText.text = "";
    }

    /// <summary>
    /// Shows the error text when the Nickname field is empty and player tries to move forward
    /// </summary>
    IEnumerator ShowNameInputError()
    {
        errorText.text = "Player NickName Missing, please check values";
        yield return new WaitForSeconds(3f);
        errorText.text = "";
    }
}
