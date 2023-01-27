using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ListOfRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _roomsContainer;
    [SerializeField] private Button _buttonTeamplate;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            _buttonTeamplate.GetComponentInChildren<Text>().text = room.Name;
            Instantiate(_buttonTeamplate);
            _buttonTeamplate.transform.SetParent(_roomsContainer);
        }
    }
}
