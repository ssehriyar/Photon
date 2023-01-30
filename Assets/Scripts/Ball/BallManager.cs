using Photon.Pun;
using System.IO;
using UnityEngine;

namespace MyGame
{
	public class BallManager : MonoBehaviour
	{
		[SerializeField] private GameObject _ballPrefab;
		[SerializeField] private PositionManger _positionManager;
		[SerializeField] private Vector3 _startVelocity;


		private void Start()
		{
			CreateBall();
		}

		private void CreateBall()
		{
			object[] instatiationData = { _startVelocity };

			PhotonNetwork.InstantiateRoomObject(
				Path.Combine("Prefabs", _ballPrefab.name),
				_positionManager.GetBallPosition,
				Quaternion.identity,
				0,
				instatiationData);
		}
	}
}