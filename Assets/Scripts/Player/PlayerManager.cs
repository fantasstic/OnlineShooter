using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    private Transform[] _spawnPoints;
    private PhotonView _photonView;
    private int _spawnPointsCount = 0;

    private void Awake()
    {
        var parentSpawnPoint = FindObjectOfType<SpawnPoints>();

        _spawnPointsCount = parentSpawnPoint.transform.childCount;

        _spawnPoints = new Transform[_spawnPointsCount];

        for (int i = 0; i < _spawnPointsCount; i++)
        {
            _spawnPoints[i] = parentSpawnPoint.transform.GetChild(i).transform;
        }
    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if( _photonView.IsMine )
            CreatePlayer();

    }

    private void CreatePlayer()
    {
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        PhotonNetwork.Instantiate("Default Player", _spawnPoints[spawnPointNumber].position, Quaternion.identity);
    }
}
