using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int _multiPlayerSceneIndex;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Transform _playersContainer;
    [SerializeField] private GameObject _playerListingPrefab;
    [SerializeField] private TMP_Text _roomNameDisplay;

    private void ClearPlayerListings()
    {
        for (int i = _playersContainer.childCount; i >= 0; i--)
        {
            Destroy(_playersContainer.GetChild(i).gameObject);
        }
    }

    private void ListPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(_playerListingPrefab, _playersContainer);
            TMP_Text tempText = tempListing.transform.GetChild(0).GetComponent<TMP_Text>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
        _roomPanel.SetActive(true);
        _lobbyPanel.SetActive(false);
        _roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            _startButton.SetActive(true);
        }
        else
        {
            _startButton.SetActive(false);
        }

        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            _startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(_multiPlayerSceneIndex);
        }
    }

    private IEnumerator ReJoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick()
    {
        _lobbyPanel.SetActive(true);
        _roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(ReJoinLobby());
    }
}
