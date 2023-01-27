using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private TMP_InputField _userRoomName;

    public void OnCreateButtonClick()
    {
        string userInput = _userRoomName.text;

        if (string.IsNullOrEmpty(userInput))
            return;

        PhotonNetwork.CreateRoom(userInput);
        
    }
}
