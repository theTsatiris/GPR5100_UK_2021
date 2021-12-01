using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListManagmenet : MonoBehaviourPunCallbacks
{
    private Dictionary<string, RoomInfo> cachedRoomList;
    // Start is called before the first frame update
    void Start()
    {
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();

        cachedRoomList = new Dictionary<string, RoomInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
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

        if(cachedRoomList.Count < 1)
        {
            Debug.Log("NO ROOMS AVAILABLE!!");
        }

        foreach(RoomInfo room in cachedRoomList.Values)
        {
            Debug.Log("Room name: " + room.Name + ", Max Players: " + room.MaxPlayers);
        }
    }
}
