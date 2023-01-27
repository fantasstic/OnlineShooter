using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _mainMenu, _loadingPanel , _roomMenu;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        _loadingPanel.SetActive(false);
        _mainMenu.SetActive(true);
        Debug.Log("Connected to lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected to the room");
        _loadingPanel.SetActive(false);
        _roomMenu.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Not connected to the room" + message);
    }
}
