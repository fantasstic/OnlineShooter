using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class RoomMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private GameObject _startButton;

    private void Start()
    {
        _roomName.text = "Room created: " + PhotonNetwork.CurrentRoom.Name;

        if(!PhotonNetwork.IsMasterClient)
            _startButton.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
