using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    private void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
