using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagement : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    
    public void CreateRoom()
    {
        if((createInput.text != "") && (createInput.text != "Room Name"))
        { 
            RoomOptions rOptions = new RoomOptions();
            rOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(createInput.text, rOptions);
        }
        else
        {
            Debug.Log("Please type a valid room name!!");
        }
    }

    public void JoinRoom()
    {
        if ((joinInput.text != "") && (joinInput.text != "Room Name"))
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.Log("Please type a valid room name!!");
        }
    }

    public override void OnJoinedRoom()
    {
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

}
