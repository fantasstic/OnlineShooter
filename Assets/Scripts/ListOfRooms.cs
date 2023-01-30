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
    [SerializeField] private GameObject _buttonTeamplate;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomList();

        foreach (RoomInfo room in roomList)
        {
            _buttonTeamplate.GetComponentInChildren<TextMeshProUGUI>().text = room.Name;
            _buttonTeamplate.GetComponent<Button>().onClick.AddListener(OnJoinRoomButtonClick);
            Instantiate(_buttonTeamplate, _roomsContainer);
        }
    }

    public void OnJoinRoomButtonClick(string roomName)
    {
        _buttonTeamplate.GetComponentInChildren<TextMeshProUGUI>().text = roomName;
        
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ClearRoomList()
    {
        foreach (Transform room in _roomsContainer)
        {
            Destroy(room.gameObject);
        }
    }
}
