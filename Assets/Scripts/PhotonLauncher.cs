using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _mainMenu, _loadingPanel;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _loadingPanel.SetActive(false);
        _mainMenu.SetActive(true);
        Debug.Log("Connected to lobby");
    }
}
