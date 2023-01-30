using UnityEngine;

namespace MyGame
{
	public class PositionManger : MonoBehaviour
	{
		public Transform _ballPosition;
		public Transform[] _playerPositions;

		public Vector3 GetBallPosition => _ballPosition.position;

		public Vector3 GetPlayerPosition(int actorNumber) => _playerPositions[actorNumber].position;
	}
}
