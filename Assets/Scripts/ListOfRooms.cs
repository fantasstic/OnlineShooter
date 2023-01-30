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
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _joinRoomPanel;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomList();

        foreach (RoomInfo room in roomList)
        {
            GameObject createdButton = Instantiate(_buttonTeamplate, _roomsContainer);
            createdButton.GetComponentInChildren<TextMeshProUGUI>().text = room.Name;
            createdButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnJoinRoomButtonClick(room.Name);
            });
        }
    }

    public void OnJoinRoomButtonClick(string roomName)
    {
        _buttonTeamplate.GetComponentInChildren<TextMeshProUGUI>().text = roomName;
        
        PhotonNetwork.JoinRoom(roomName);
        _loadingScreen.SetActive(true);
        _joinRoomPanel.SetActive(false);
    }

    public void ClearRoomList()
    {
        foreach (Transform room in _roomsContainer)
        {
            Destroy(room.gameObject);
        }
    }
}
