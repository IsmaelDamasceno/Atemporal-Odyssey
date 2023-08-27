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

		public float MoveSpeed;

		[SerializeField] private InputMember _inputMember;
		private Rigidbody2D _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_inputMember = GetComponent<InputMember>();
		}

		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}

}
