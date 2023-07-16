using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class MoveOrgan : BaseOrgan
	{

		[SerializeField] private float _moveSpeed;

		private InputOrgan _inputOrgan;
		private Rigidbody2D _rigidbody;

		void Start()
		{
			_inputOrgan = transform.parent.GetComponent<InputOrgan>();
			_rigidbody = GetRoot().GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(_moveSpeed * _inputOrgan.MoveInput, _rigidbody.velocity.y);
		}
	}

}