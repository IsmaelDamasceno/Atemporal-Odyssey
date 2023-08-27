using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla o sistema de movimento do jogador
	/// </summary>
	public class MoveMember : BaseMember
	{

		[SerializeField] public float MoveSpeed;

		private InputMember _inputMember;
		private Rigidbody2D _rigidbody;

		private void Awake()
		{
            _inputMember = GetComponent<InputMember>();
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}

}
