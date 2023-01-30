using Photon.Pun;
using System;
using UnityEngine;

namespace MyGame
{
	public class Ball : MonoBehaviour/*, IPunObservable*/
	{
		private Rigidbody _rigidbody;
		private Vector3 _position;
		private float _yPos;
		private PhotonView _photonView;
		private Vector3 _velocity;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_photonView = GetComponent<PhotonView>();
		}

		private void Start()
		{
			//_rigidbody.velocity = _startVelocity;
			_yPos = transform.position.y;
			if (_photonView.InstantiationData != null)
			{
				_velocity = (Vector3)_photonView.InstantiationData[0];
			}
			_rigidbody.AddForce(_velocity, ForceMode.VelocityChange);
		}

		private void FixedUpdate()
		{
			if (_rigidbody.velocity.magnitude != _velocity.magnitude)
			{
				_rigidbody.velocity = _velocity;
			}
			//Debug.Log(_rigidbody.velocity);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.GetComponent<Goal>() != null)
			{
				ScoreManager.Instance.AddScore(collision.gameObject.GetComponent<Goal>().player);
			}
			//OnHit();
		}

		//private void OnHit()
		//{
		//	photonView.RPC("OnHitRPC", RpcTarget.All, transform.position);
		//}

		//[PunRPC]
		//private void OnHitRPC(Vector3 pos)
		//{
		//	Debug.Log("Hit!!");
		//	transform.position = pos;
		//}

		//public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
		//{
		//	if (stream.IsWriting)
		//	{
		//		stream.SendNext(transform.position.x);
		//		stream.SendNext(transform.position.z);
		//	}
		//	else
		//	{
		//		float x = (float)stream.ReceiveNext();
		//		float z = (float)stream.ReceiveNext();
		//		Debug.Log($"x: {x} - z: {z}");
		//		_position = new Vector3(x, _yPos, z);
		//		transform.position = _position;
		//	}
		//}
	}
}