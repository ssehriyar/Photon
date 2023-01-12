using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{
    private int _roomSize;
    private string _roomName;
    private List<RoomInfo> _roomListings;

    [SerializeField] private GameObject _lobbyConnectButton;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private InputField _playerNameInput;
    [SerializeField] private GameObject _roomListingPrefab;
    [SerializeField] private Transform _roomsContainer;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _lobbyConnectButton.SetActive(true);
        _roomListings = new List<RoomInfo>();

        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        }
        _playerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }

    public void JoinLobbyOnClick()
    {
        _mainPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (_roomListings != null)
            {
                tempIndex = _roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1)
            {
                _roomListings.RemoveAt(tempIndex);
                Destroy(_roomsContainer.GetChild(tempIndex).gameObject);
            }
            if (room.PlayerCount > 0)
            {
                _roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    private static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    private void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(_roomListingPrefab, _roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        _roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        _roomSize = int.Parse(sizeIn);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room");
        RoomOptions roomOptins = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)_roomSize
        };
        PhotonNetwork.CreateRoom(_roomName, roomOptins);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
    }

    public void MatchmakingCancel()
    {
        _mainPanel.SetActive(true);
        _lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
