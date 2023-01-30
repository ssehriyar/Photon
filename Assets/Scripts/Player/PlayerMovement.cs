using UnityEngine;
using Photon.Pun;

namespace MyGame
{
	public class PlayerMovement : MonoBehaviourPunCallbacks
	{
		private Vector3 _moveDir;
		[SerializeField] private float _moveSpeed;

		private void Awake()
		{
			if (photonView.IsMine == false && GetComponent<PlayerMovement>() != null)
			{
				Debug.Log("DELETED");
				Destroy(GetComponent<PlayerMovement>());
			}
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			_moveDir = Vector3.zero;

			if (Input.GetKey(KeyCode.W)) _moveDir.z = +1f;
			if (Input.GetKey(KeyCode.S)) _moveDir.z = -1f;
			//if (Input.GetKey(KeyCode.A)) _moveDir.x = -1f;
			//if (Input.GetKey(KeyCode.D)) _moveDir.x = +1f;

			transform.position += _moveSpeed * Time.deltaTime * _moveDir;
		}
	}
}
