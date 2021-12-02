using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManagement : MonoBehaviourPunCallbacks
{
    public static byte MAX_PLAYERS = 50;

    public InputField createInput;
    public InputField playerNum;
    public InputField nickName;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(createInput.text) && (createInput.text != "Room Name"))
        { 
            if(NickNameIsValid())
            {
                byte playerNumber;
                if(!string.IsNullOrEmpty(playerNum.text) && byte.TryParse(playerNum.text, out playerNumber))
                { 
                    PhotonNetwork.NickName = nickName.text;
                    RoomOptions rOptions = new RoomOptions();
                    rOptions.IsOpen = true;
                    rOptions.IsVisible = true;
                    if (playerNumber > MAX_PLAYERS)
                        rOptions.MaxPlayers = MAX_PLAYERS;
                    else if (playerNumber < 2)
                        rOptions.MaxPlayers = 2;
                    else
                        rOptions.MaxPlayers = playerNumber;
                    PhotonNetwork.CreateRoom(createInput.text, rOptions);
                }
            }
            else
            {
                Debug.Log("Please type a valid nickname!!");
            }
        }
        else
        {
            Debug.Log("Please type a valid room name!!");
        }
    }

    public void JoinRoom()
    {
        if (NickNameIsValid())
        {
            PhotonNetwork.NickName = nickName.text;
            PhotonNetwork.LoadLevel("RoomListScene");
        }
    }

    public void JoinRandom()
    {
        if (NickNameIsValid())
        {
            PhotonNetwork.NickName = nickName.text;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Please type a valid nickname!!");
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
            PhotonNetwork.LoadLevel("WaitingForPlayers");
        else
            PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("ERROR! Failed to create room!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR! Failed to join room!");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions rOptions = new RoomOptions();
        rOptions.IsOpen = true;
        rOptions.IsVisible = true;
        rOptions.MaxPlayers = (byte)Random.Range(2, MAX_PLAYERS);

        string roomName = "Room" + Random.Range(0, 100000);
        PhotonNetwork.CreateRoom(roomName, rOptions);   
    }

    private bool NickNameIsValid()
    {
        if (string.IsNullOrEmpty(nickName.text) || (nickName.text == "Enter Nickname..."))
            return false;
        else
            return true;
    }

}
