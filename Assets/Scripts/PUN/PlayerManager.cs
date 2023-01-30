using Photon.Pun;
using System.IO;
using UnityEngine;

namespace MyGame
{
	public class PlayerManager : MonoBehaviour
	{
		[SerializeField] private GameObject _playerPrefab;
		[SerializeField] private PositionManger _positionManager;

		private void Start()
		{
			CreatePlayer();
		}

		private void CreatePlayer()
		{
			PhotonNetwork.Instantiate(
				Path.Combine("Prefabs", _playerPrefab.name),
				_positionManager.GetPlayerPosition(PhotonNetwork.LocalPlayer.ActorNumber - 1),
				Quaternion.identity);
		}
	}
}