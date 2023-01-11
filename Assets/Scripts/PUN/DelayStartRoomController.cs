using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int _waitingRoomSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SceneManager.LoadScene(_waitingRoomSceneIndex);
    }
}
