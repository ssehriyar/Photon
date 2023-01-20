using Photon.Pun;
using Photon.Realtime;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;

    private bool _readyToCountDown;
    private bool _readyToStart;
    private bool _startingGame;

    private int _playerCount;
    private int _roomSize;

    private float _timerToStartGame;
    private float _notFullGameTimer;
    private float _fullGameTimer;

    [SerializeField] private int _multiplayerSceneIndex;
    [SerializeField] private int _menuSceneIndex;
    [SerializeField] private int _minPlayerToStart;

    [SerializeField] private TMP_Text _roomCountText;
    [SerializeField] private TMP_Text _timerToStartText;

    [SerializeField] private float _maxWaitTime;
    [SerializeField] private float _maxFullGameWaitTime;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _fullGameTimer = _maxFullGameWaitTime;
        _notFullGameTimer = _maxWaitTime;
        _timerToStartGame = _maxWaitTime;

        PlayerCountUpdate();
    }

    private void PlayerCountUpdate()
    {
        _playerCount = PhotonNetwork.PlayerList.Length;
        _roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        _roomCountText.text = _playerCount + ":" + _roomSize;

        if (_playerCount == _roomSize)
        {
            _readyToStart = true;
        }
        else if (_playerCount >= _minPlayerToStart)
        {
            _readyToStart = true;
        }
        else
        {
            _readyToCountDown = false;
            _readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        if (PhotonNetwork.IsMasterClient)
        {
            _photonView.RPC("RPC_SendTimer", RpcTarget.Others, _timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        _timerToStartGame = timeIn;
        _notFullGameTimer = timeIn;
        if (timeIn < _fullGameTimer)
        {
            _fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    private void WaitingForMorePlayers()
    {
        if (_playerCount <= 1)
        {
            ResetTimer();
        }

        if (_readyToStart)
        {
            _fullGameTimer -= Time.deltaTime;
            _timerToStartGame = _fullGameTimer;
        }
        else if (_readyToCountDown)
        {
            _notFullGameTimer -= Time.deltaTime;
            _timerToStartGame = _notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", _timerToStartGame);
        _timerToStartText.text = tempTimer;

        if (_timerToStartGame <= 0f)
        {
            if (_startingGame) return;

            StartGame();
        }
    }

    private void StartGame()
    {
        _startingGame = true;
        if (PhotonNetwork.IsMasterClient == false) return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(_multiplayerSceneIndex);
    }

    private void ResetTimer()
    {
        _timerToStartGame = _maxWaitTime;
        _notFullGameTimer = _maxWaitTime;
        _fullGameTimer = _maxFullGameWaitTime;
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(_menuSceneIndex);
    }
}
