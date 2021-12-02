using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomListManagmenet : MonoBehaviourPunCallbacks
{
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObjects;

    public GameObject roomItemPrefab;
    public GameObject roomItemParent;

    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObjects = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(GameObject listObj in roomListGameObjects.Values)
        {
            Destroy(listObj);
        }
        roomListGameObjects.Clear();


        foreach(RoomInfo room in roomList)
        {
            //Remove room from cache if it's not available
            if(!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }
            }
            else
            { 
                //Update room if already in cache
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList[room.Name] = room;
                }
                //Add room if not in cache
                else
                {
                    cachedRoomList.Add(room.Name, room);
                }
            }
        }

        foreach (RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomItem = Instantiate(roomItemPrefab, roomItemParent.transform);
            roomItem.transform.localScale = Vector3.one;

            roomItem.transform.Find("RoomName").GetComponent<TMP_Text>().text = room.Name;
            roomItem.transform.Find("PlayerNumber").GetComponent<TMP_Text>().text = "Player: " + room.PlayerCount + "/" + room.MaxPlayers;
            roomItem.transform.Find("JoinButton").GetComponent<Button>().onClick.AddListener(() => JoinButtonClick(room.Name));

            roomListGameObjects.Add(room.Name, roomItem);
        }

        //if(cachedRoomList.Count < 1)
        //{
        //    Debug.Log("NO ROOMS AVAILABLE!!");
        //}

        //foreach(RoomInfo room in cachedRoomList.Values)
        //{
        //    Debug.Log("Room name: " + room.Name + ", Max Players: " + room.MaxPlayers);
        //}
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
            PhotonNetwork.LoadLevel("WaitingForPlayers");
        else
            PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("ERROR! Failed to join room!");
    }

    private void JoinButtonClick(string roomName)
    {
        if (PhotonNetwork.InLobby)
            PhotonNetwork.LeaveLobby();

        PhotonNetwork.JoinRoom(roomName);
    }
}
